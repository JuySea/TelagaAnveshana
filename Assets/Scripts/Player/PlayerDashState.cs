using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    private float dodgeDistance = 8f;

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);

    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(dodgeDistance * player.facingDir, rb.velocity.y);

        if (triggerCalled) 
            stateMachine.ChangeState(player.idleState);
        

        if (!player.IsGrounded() && player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlide);
    }
}
