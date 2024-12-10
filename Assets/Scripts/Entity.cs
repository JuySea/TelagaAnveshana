using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [Header("Ground Check")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallDistance;
    [SerializeField] protected LayerMask layerMask;

    [Header("Battle Info")]
    public Transform attackCheck;
    public float attackCheckRadius = 0.8f;
    [SerializeField] protected Vector2 knockbackDirection;
    protected bool isKnocked;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    #region Components
    public SpriteRenderer sr { get; private set; }
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx {  get; private set; } 
    public CharacterStats stats { get; private set; }
    public CapsuleCollider2D cd { get; private set; }
    #endregion

    public System.Action onFlipped;
    public System.Action onDied;

    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponentInChildren<EntityFX>();
        stats = GetComponent<CharacterStats>();
        cd = GetComponent<CapsuleCollider2D>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        
    }

    public virtual void SlowEntity(float slowPercentage, float slowDuration)
    {

    }

    protected virtual void ReturnDefaultSpeed()
    {
        anim.speed = 1;
    }

    public virtual void DamageEffect()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("KnockBack");
    }

    protected virtual IEnumerator KnockBack()
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);

        yield return new WaitForSeconds(.4f);

        isKnocked = false;
    }

    #region Flip
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if(onFlipped != null) 
            onFlipped();
    }

    public void FlipController(float xInput)
    {
        if (xInput < 0 && facingRight)
        {
            Flip();
        }
        else if (xInput > 0 && !facingRight)
        {
            Flip();
        }
    }
    #endregion 

    public void MakeTransparent(bool isTransparent)
    {
        if (isTransparent)
        {
            sr.color = Color.clear;
        }
        else
        {
            sr.color = Color.white;
        }
    }

    #region Velocity
    public void SetVelocity(float xInput, float yInput)
    {

        if (isKnocked)
            return;

        rb.velocity = new Vector2(xInput, yInput);
        FlipController(xInput);
    }
    public void ZeroVelocity()
    {

        if (isKnocked)
            return;

        rb.velocity = new Vector2(0, 0);
    }
    #endregion

    #region Physics2D
    public bool IsGrounded() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundDistance, layerMask);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallDistance, layerMask);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

    public virtual void Die()
    {
        if (onDied != null)
            onDied();
    }
}
