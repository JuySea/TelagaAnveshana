using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    #region State Machine
    public SkeletonIdleState idleState;
    public SkeletonMoveState moveState;
    public SkeletonBattleState battleState;
    public SkeletonAttackState attackState;
    public SkeletonStunnedState stunnedState;
    public SkeletonDeadState deadState;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine,"Idle", this);
        moveState = new SkeletonMoveState(this, stateMachine,"Move", this);
        battleState = new SkeletonBattleState(this, stateMachine,"Move", this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        stunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
        deadState = new SkeletonDeadState(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Intialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        //Debug.Log(CanBeStunned());
    }

    public override bool CanBeStunned()
    {
        if (base.isCanBeStunned)
        {
            CloseCounterAttackWindow();
            stateMachine.ChangeState(stunnedState); 
            return true;
        }
        else
        {
            return false;
        }
    }
    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }
}
