using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMachine stateMachine;

    [SerializeField] protected LayerMask playerLayer;
    
    [Header("Move Info")]
    public float moveSpeed = 2;
    public float idleTime = 2;
    public float battleTime = 8;
    public float battleRange = 7;
    private float defaultMoveSpeed;

    [Header("Attack Info")]
    public float attackDistance = 1.5f;
    public float attackCooldown = 0.4f;
    [HideInInspector] public float lastTimeAttack;

    protected bool isCanBeStunned;
    [SerializeField] protected GameObject counterWindow;
    public string lastAnimBoolName {  get; protected set; }
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

        defaultMoveSpeed = moveSpeed;
    }

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

    }

    public override void SlowEntity(float slowPercentage, float slowDuration)
    {
        moveSpeed *= (1 - slowPercentage);
        anim.speed *= (1 - slowPercentage);

        Invoke("ReturnDefaultSpeed", slowDuration);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
    }

    public virtual void AssignLastAnimName(string animBoolName)
    {
        lastAnimBoolName = animBoolName;
    }

    public void FreezeTime(bool timeFrozen)
    {
        if (timeFrozen)
        {
            moveSpeed = 0;
            anim.speed = 0;
        }
        else
        {
            moveSpeed = defaultMoveSpeed;
            anim.speed = 1;
        }
    }

    protected virtual IEnumerator FreezeTimeFor(float seconds)
    {
        FreezeTime(true);

        yield return new WaitForSeconds(seconds);

        FreezeTime(false);
    }

    #region Stunned
    public virtual void OpenCounterAttackWindow()
    {
        isCanBeStunned = true;
        counterWindow.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        isCanBeStunned = false;
        counterWindow.SetActive(false);
    }

    public virtual bool CanBeStunned()
    {
        if(isCanBeStunned)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion
    
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 30, playerLayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }


}
