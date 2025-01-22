using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public Player player {  get; private set; }
    public EnemyStats stats { get; private set; }
    public CapsuleCollider2D cd { get; private set; }
    public SpriteRenderer sr { get; private set; }

    [Header("Ground Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform frontCheck;
    [SerializeField] protected Transform attackCheck;
    
    protected float groundCheckDistance = 0.15f;
    protected float frontDistance = .2f;
    protected float attackCheckRadius = 0.8f;
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] private LayerMask playerLayer;

    protected float blinkDuration = 1.0f; 
    protected float blinkInterval = 0.1f;
    protected float noiseCooldown = 5f;
    protected float lastNoiseTime;




    [Header("Move Info")]
    public float moveSpeed = 2f;
    protected float lastTimeIdle;
    protected float idleTime = 1.5f;
    protected int facingDir = -1;
    protected bool facingLeft = true;
    protected bool triggerCalled = true;

    [Header("Attack Info")]
    protected float lastTimeAttack;
    protected float attackCooldown;

    [Header("Hit Info")]
    protected bool isKnocked;
    protected Vector2 knockbackDirection = new Vector2(4, 1.5f);

    public System.Action onFlipped;
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stats = GetComponent<EnemyStats>();
        cd = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
        lastTimeIdle = Time.time;
        lastNoiseTime = -10;

    }

    protected virtual void Update()
    {

        if (Vector2.Distance(player.transform.position, transform.position) <= 5)
        {
            if (Time.time > lastNoiseTime + noiseCooldown)
            {
                Noise();
            }
        }

    }

    protected virtual bool IsGrounded() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, layerMask);
    protected virtual bool IsHindered() => Physics2D.Raycast(frontCheck.position, Vector2.left * facingDir, frontDistance, layerMask);
    protected virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(frontCheck.position, Vector2.left * facingDir, 20, playerLayer);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(frontCheck.position, new Vector2(frontCheck.position.x - frontDistance, frontCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }

    protected virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingLeft = !facingLeft;
        transform.Rotate(0, 180, 0);

        if(onFlipped != null)
            onFlipped();
    }

    protected virtual void Move(float speed, bool animBool)
    {
        moveSpeed = speed;
        rb.velocity = new Vector2(facingDir * moveSpeed, rb.velocity.y);
        anim.SetBool("Move", animBool);
    }
    protected virtual void Patrol()
    {
        if (triggerCalled)
        {
            if (Time.time > lastTimeIdle + idleTime)
            {

                Move(2, true);
            }

            if (!IsGrounded() || IsHindered())
            {
                if (rb.velocity.y == 0)
                    Flip();

                Move(0, false);
                lastTimeIdle = Time.time;
                if (IsGrounded() && IsHindered())
                {
                    Flip();
                }
            }
        }

    }

    protected virtual void Movement()
    {

    }

    public virtual void Die()
    {
        Move(0, false);
        anim.SetBool("Die", true);
        StartCoroutine("BlinkAndDisappear");

    }


    protected virtual IEnumerator KnockBack()
    {
        isKnocked = true;
        anim.SetTrigger("Knocked");
        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);

        yield return new WaitForSeconds(.4f);
        rb.velocity = new Vector2(0, rb.velocity.y);
        isKnocked = false;
    }

    public virtual void Damaged()
    {
        StartCoroutine("KnockBack");
    }

    protected virtual void Noise()
    {
    }


    #region AnimationTrigger

    protected virtual void AnimationEnd()
    {
        triggerCalled = true;
    }

    protected virtual void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                if(hit.GetComponent<Player>().IsDead == false)
                {
                    if (player.IsInvincible)
                        return;
                    stats.DoDamage(hit.GetComponent<PlayerStats>());
                    hit.GetComponent<PlayerStats>().TakeDamage(10);

                }
            }
        }
    }

    #endregion

    private IEnumerator BlinkAndDisappear()
    {
        float elapsedTime = 0f;
        bool isVisible = true;

        while (elapsedTime < blinkDuration)
        {
            isVisible = !isVisible;
            SetVisibility(isVisible);

            yield return new WaitForSeconds(blinkInterval);

            elapsedTime += blinkInterval;
        }
        SetVisibility(false);
        Destroy(gameObject);
    }

    private void SetVisibility(bool visible)
    {
        sr.enabled = visible;
    }
}
