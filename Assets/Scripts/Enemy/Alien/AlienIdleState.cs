using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienIdleState : AlienGroundedState
{
    public AlienIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Alien alien) : base(enemy, stateMachine, animBoolName, alien)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = alien.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(alien.moveState);
    }
}
