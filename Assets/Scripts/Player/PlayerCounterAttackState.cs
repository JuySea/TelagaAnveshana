using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = .9f;
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void Update()
    {
        base.Update();
     

        if( triggerCalled)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null)
                {
                    if (hit.GetComponent<Enemy>().CanBeStunned())
                    {
                        stateTimer = 10;
                        stateMachine.ChangeState(player.counterAttackSuccessful);

                    }
                    else
                    {
                        stateMachine.ChangeState(player.idleState);

                    }
                }



            }
            if (colliders.Length == 0)
            {
                Debug.Log("ASDASDASD");

                stateMachine.ChangeState(player.idleState);

            }

        }
    }
}
