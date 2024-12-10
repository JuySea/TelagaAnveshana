using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGroundedState : EnemyState
{
    protected Enemy_Alien alien;
    protected Transform player;
    public AlienGroundedState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Alien alien) : base(enemy, stateMachine, animBoolName)
    {
        this.alien = alien;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (alien.IsPlayerDetected() || Vector2.Distance(player.position, alien.transform.position) < (alien.battleRange / 2))
        {
            stateMachine.ChangeState(alien.battleState);
        }
    }
}
