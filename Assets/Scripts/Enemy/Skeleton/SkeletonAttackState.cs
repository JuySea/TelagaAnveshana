using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    private Enemy_Skeleton skeleton;
    public SkeletonAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animBoolName)
    {
        this.skeleton = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.lastTimeAttack = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.ChangeState(skeleton.battleState);
        }
    }
}
