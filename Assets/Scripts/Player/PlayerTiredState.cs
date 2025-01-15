using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTiredState : PlayerState

{
    public PlayerTiredState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.rb.velocity = new Vector2(0,rb.velocity.y);

        if(triggerCalled || PlayerManager.instance.stats.currentHealth <= 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
