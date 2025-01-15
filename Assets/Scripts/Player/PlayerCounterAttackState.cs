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
    }

    public override void Exit()
    {
        base.Exit();
        player.counterCooldown = .29f;
    }

    public override void Update()
    {
        base.Update();

        player.rb.velocity = new Vector2(0,rb.velocity.y);
     

        if( triggerCalled)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

            if (colliders.Length < 1 ) 
            {
                stateMachine.ChangeState(player.idleState);
            }

            foreach (var hit in colliders)
            {
                if (hit.GetComponent<Enemy>() != null)
                {
                    if (hit.GetComponent<Enemy>().CanBeStunned() && player.counterCooldown < 0)
                    {
                        stateTimer = 10;
                        stateMachine.ChangeState(player.counterAttackSuccessful);

                    }
                    else
                    {
                        stateMachine.ChangeState(player.idleState);

                    }
                }
                else
                {
                    stateMachine.ChangeState(player.idleState);

                }
            }


        }
    }
}
