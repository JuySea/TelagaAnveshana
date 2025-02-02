using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackSuccessfulState : PlayerState
{
    public PlayerCounterAttackSuccessfulState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.anim.SetBool("CounterAttack", false);
        
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {

            stateMachine.ChangeState(player.idleState);
        }
    }
}