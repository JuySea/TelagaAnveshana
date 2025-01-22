using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(2);
        }

        if (!player.IsGrounded())
            stateMachine.ChangeState(player.airState);

        if (xInput == 0)
            player.SetVelocity(0, rb.velocity.y);

        if (player.IsTired)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse1)) 
            stateMachine.ChangeState(player.counterAttack);

        if (Input.GetKeyDown(KeyCode.W) && player.IsGrounded())
            stateMachine.ChangeState(player.jumpState);


        if (Input.GetMouseButtonDown(2) && HasNoSword())
        {
            if (PlayerManager.instance.stats.stamina < 7)
                return;
            stateMachine.ChangeState(player.aimSword);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (PlayerManager.instance.stats.stamina < 5)
            {
                return;
            }
            stateMachine.ChangeState(player.primaryAttack);
        }
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
