using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienAttackState : EnemyState
{
    private Enemy_Alien alien;
    public AlienAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName, Enemy_Alien alien) : base(enemy, stateMachine, animBoolName)
    {
        this.alien = alien;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        alien.lastTimeAttack = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.ChangeState(alien.battleState);
        }
    }
}
