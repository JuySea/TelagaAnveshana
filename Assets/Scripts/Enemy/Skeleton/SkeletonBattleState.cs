using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Enemy_Skeleton skeleton;
    private Transform player;
    private int moveDir;
    public SkeletonBattleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animBoolName)
    {
        this.skeleton = skeleton;
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

        if(skeleton.IsPlayerDetected())
        {
            stateTimer = skeleton.battleTime;
            if (skeleton.IsPlayerDetected().distance <= skeleton.attackDistance && CanAttack())
            {
                stateMachine.ChangeState(skeleton.attackState);
            }
        }
        else
        {
            if(stateTimer < 0 || Vector2.Distance(player.position,skeleton.transform.position) < skeleton.battleRange) 
            {
                stateMachine.ChangeState(skeleton.idleState);
            }
        }

        if (player.position.x > skeleton.transform.position.x)
        {
            moveDir = 1;
        } 
        else
        {
            moveDir = -1;
        }

        skeleton.SetVelocity(moveDir * skeleton.moveSpeed, rb.velocity.y);
    }

    private bool CanAttack()
    {
        if(Time.time > skeleton.lastTimeAttack + skeleton.attackCooldown)
        {
            return true;
        }
        return false;
    }
}
