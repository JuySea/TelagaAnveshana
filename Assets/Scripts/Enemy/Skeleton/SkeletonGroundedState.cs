using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected Enemy_Skeleton skeleton;
    protected Transform player;
    public SkeletonGroundedState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Skeleton skeleton) : base(enemy, stateMachine, animBoolName)
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

        if (skeleton.IsPlayerDetected() || Vector2.Distance(player.position, skeleton.transform.position) < (skeleton.battleRange / 2))
        {
            stateMachine.ChangeState(skeleton.battleState);
        }
    }
}
