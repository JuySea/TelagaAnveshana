using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    private int comboCounter;
    private float lastTimeAttack;
    private float comboWindow = 2f;

    public override void Enter()
    {
        base.Enter();
        PlayerManager.instance.stats.StaminaUsed(7);
        xInput = 0;

        float attackDir = player.facingDir;

        if (xInput != 0)
            attackDir = xInput;

        if(comboCounter > 2 || Time.time >= lastTimeAttack + comboWindow)
        {
            comboCounter = 0;
        }
        player.anim.SetInteger("ComboCounter", comboCounter);
        player.SetVelocity(player.attackMovement[0].x * attackDir, player.attackMovement[0].y);

        stateTimer = .15f;
    }

    public override void Exit()
    {
        base.Exit();

        comboCounter++;
        lastTimeAttack = Time.time;

        player.StartCoroutine("BusyFor", .15f);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            rb.velocity = new Vector2(0, 0);       

        if(triggerCalled)
           stateMachine.ChangeState(player.idleState);
        
    }
}
