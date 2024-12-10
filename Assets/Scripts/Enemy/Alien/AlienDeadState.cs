using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienDeadState : EnemyState
{
    private Enemy_Alien alien;
    public AlienDeadState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Alien alien) : base(enemy, stateMachine, animBoolName)
    {
        this.alien = alien;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.anim.SetBool("Died", true);
        //enemy.anim.speed = 0;
        //enemy.cd.enabled = false;

        stateTimer = .15f;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
        {
        }


        // Enemy died and out of frame:
        //if (stateTimer > 0)
        //{
        //    rb.velocity = new Vector2(0, 10);
        //}
    }
}
