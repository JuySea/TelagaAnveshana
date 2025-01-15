using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState

{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.R))
            stateMachine.ChangeState(player.blackhole);

        if(Input.GetKeyDown(KeyCode.Mouse1) && HasNoSword()) 
            stateMachine.ChangeState(player.aimSword);
        
        if(Input.GetKeyDown(KeyCode.U))
            stateMachine.ChangeState(player.counterAttack);

        if (xInput == 0)
            player.SetVelocity(0, rb.velocity.y);

        if (!player.IsGrounded())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKey(KeyCode.H))
            stateMachine.ChangeState(player.primaryAttack);

        if (Input.GetKeyDown(KeyCode.W) && player.IsGrounded())
            stateMachine.ChangeState(player.jumpState);
    }

    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }

        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
