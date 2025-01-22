using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : Entity
{
    [Header("Move Info")]
    public float moveSpeed = 8f;
    public float jumpForce = 12f;
    public float dashDuration = 0.15f;
    public float dashSpeed = 2;
    public float staminaUsedForDash = 15;
    public bool IsDead = false;
    public bool IsInvincible = false;

    public float counterCooldown;
    private float defaultMoveSpeed;
    private float defaultJumpForce;
    private float defaultDashSpeed;
    public float dashDir {  get; private set; }
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float dashCooldownTimer;
    public bool IsBusy { get; private set; } = false;
    public bool IsTired = false;
    public Vector2[] attackMovement;
    public GameObject sword { get; private set; }
    public float swordReturnImpact = 5f;

    #region StateMachine
    public PlayerStateMachine stateMachine;
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState {  get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerDeadState deadState { get; private set; }


    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }
    public PlayerCounterAttackSuccessfulState counterAttackSuccessful { get; private set; }

    public PlayerAimSwordState aimSword { get; private set; }
    public PlayerCatchSwordState catchSword { get; private set; }
    public PlayerTiredState tiredState { get; private set; }

    #endregion

    #region Skills
    public SkillManager skill { get; private set; }
    #endregion


    protected override void Awake() 
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "Wall");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        counterAttackSuccessful = new PlayerCounterAttackSuccessfulState(this, stateMachine, "SuccessfulCounterAttack");
        deadState = new PlayerDeadState(this, stateMachine, "Die");

        aimSword = new PlayerAimSwordState(this, stateMachine, "AimSword");
        catchSword = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
        tiredState = new PlayerTiredState(this, stateMachine, "Tired");
    }

    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;
        stateMachine.Initialize(idleState);

        defaultMoveSpeed = moveSpeed;
        defaultJumpForce = jumpForce;
        defaultDashSpeed = dashSpeed;
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckDashInput();
    }

    public override void SlowEntity(float slowPercentage, float slowDuration)
    {
        moveSpeed *= (1 - slowPercentage);
        jumpForce *= (1 - slowPercentage);
        dashSpeed *= (1 - slowPercentage);
        anim.speed *= (1 - slowPercentage);

        Invoke("ReturnDefaultSpeed", slowDuration);
    }

    protected override void ReturnDefaultSpeed()
    {
        base.ReturnDefaultSpeed();

        moveSpeed = defaultMoveSpeed;
        jumpForce = defaultJumpForce;
        dashSpeed = defaultDashSpeed;
    }

    public IEnumerator BusyFor(float _seconds)
    {
        IsBusy = true;

        yield return new WaitForSeconds(_seconds);

        IsBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    private void CheckDashInput()
    {
        if (PlayerManager.instance.stats.stamina < staminaUsedForDash)
        {
            return;
        }

        if (IsWallDetected())
            return;
        
        dashCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer < 0)
        {
            dashCooldownTimer = dashCooldown;

            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;
            PlayerManager.instance.stats.StaminaUsed(staminaUsedForDash);
            IsInvincible = true;
            stateMachine.ChangeState(dashState);
        }
    }

    public void AssignSword(GameObject _sword)
    {
        sword = _sword;
    }

    public void CatchTheSword()
    {
        stateMachine.ChangeState(catchSword);
        Destroy(sword);
    }

    public void Tired()
    {
        IsTired = true;
        stateMachine.ChangeState(tiredState);
    }

    public override void Die()
    {
        base.Die();
        IsDead = true;

        stateMachine.ChangeState(deadState);
    }
}
