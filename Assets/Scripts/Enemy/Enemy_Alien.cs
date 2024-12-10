using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Enemy_Alien : Enemy
{
    #region State Machine
    public AlienIdleState idleState;
    public AlienMoveState moveState;
    public AlienBattleState battleState;
    public AlienAttackState attackState;
    public AlienStunnedState stunnedState;
    public AlienDeadState deadState;
    #endregion


    protected override void Awake()
    {
        base.Awake();

        idleState = new AlienIdleState(this, stateMachine,"Idle", this);
        moveState = new AlienMoveState(this, stateMachine,"Move", this);
        battleState = new AlienBattleState(this, stateMachine,"Move", this);
        attackState = new AlienAttackState(this, stateMachine, "Attack", this);
        stunnedState = new AlienStunnedState(this, stateMachine, "Stunned", this);
        deadState = new AlienDeadState(this, stateMachine, "Died", this);
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

    public void Dissolve()
    {
        Destroy(gameObject);
    }
}
