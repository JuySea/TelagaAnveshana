using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWander : EnemyController
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        moveSpeed = 1.5f;
        attackCooldown = .2f;
        attackCheck.position = transform.position;
    }

    protected override void Update()
    {
        base.Update();
        Movement();
    }

    protected override void Movement()
    {
        if (stats.isDead)
            return;

        Patrol();
        Instill_Terror(); 
    }

    private void Instill_Terror()
    {
        if (Vector2.Distance(player.transform.position, transform.position) <= attackCheckRadius && Time.time > lastTimeAttack + attackCooldown)
        {
            AttackTrigger();
            lastTimeAttack = Time.time;
        }
    }

    protected override void Noise()
    {

    }

    public override void Damaged()
    {

    }

    protected override void AttackTrigger()
    {
        base.AttackTrigger();
    }

}
