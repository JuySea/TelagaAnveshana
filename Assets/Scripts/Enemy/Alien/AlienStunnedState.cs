using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienStunnedState : EnemyState
{
    private Enemy_Alien alien;
    public AlienStunnedState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Alien alien) : base(enemy, stateMachine, animBoolName)
    {
        this.alien = alien;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f;

        enemy.fx.InvokeRepeating("RedColorBlink",0,.1f);

        rb.velocity = new Vector2 (3 * -enemy.facingDir, 7);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.fx.Invoke("CancelColorChange", 0);

    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
        {
            stateMachine.ChangeState(alien.idleState);
        }
    }
}