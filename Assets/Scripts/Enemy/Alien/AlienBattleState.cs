using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienBattleState : EnemyState
{
    private Enemy_Alien alien;
    private Transform player;
    private int moveDir;
    public AlienBattleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Alien alien) : base(enemy, stateMachine, animBoolName)
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

        if(alien.IsPlayerDetected())
        {
            stateTimer = alien.battleTime;
            if (alien.IsPlayerDetected().distance <= alien.attackDistance && CanAttack())
            {
                stateMachine.ChangeState(alien.attackState);
            }
        }
        else
        {
            if(stateTimer < 0 || Vector2.Distance(player.position, alien.transform.position) < alien.battleRange) 
            {
                stateMachine.ChangeState(alien.idleState);
            }
        }

        if (player.position.x > alien.transform.position.x)
        {
            moveDir = 1;
        } 
        else
        {
            moveDir = -1;
        }

        alien.SetVelocity(moveDir * alien.moveSpeed, rb.velocity.y);
    }

    private bool CanAttack()
    {
        if(Time.time > alien.lastTimeAttack + alien.attackCooldown)
        {
            return true;
        }
        return false;
    }
}
