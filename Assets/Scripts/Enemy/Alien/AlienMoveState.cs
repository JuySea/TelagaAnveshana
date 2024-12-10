using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMoveState : AlienGroundedState
{
    public AlienMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Alien alien) : base(enemy, stateMachine, animBoolName, alien)
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

        alien.SetVelocity(alien.facingDir * alien.moveSpeed, rb.velocity.y);

        if (!alien.IsGrounded() || alien.IsWallDetected())
        {
            alien.Flip();
            stateMachine.ChangeState(alien.idleState);
        }
    }
}
