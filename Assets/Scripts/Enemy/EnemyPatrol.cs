using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : EnemyController
{
    [Header("Attack Info")]
    [SerializeField] private float battleRange = 12f;
    [SerializeField] private float attackDistance = 1.6f;
    protected bool attackBegin = false;
    protected bool nearTarget;
    private SlashController slash;
    
    protected override void Awake()
    {
        base.Awake();
        slash = GetComponentInChildren<SlashController>();
    }
    protected override void Start()
    {
        base.Start();
        attackCooldown = 2f;
    }

    protected override void Update()
    {
        base.Update();

        if (!isKnocked)
        {
            Movement();
        }

    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

    }

    protected virtual void FlipController()
    {
        if (!attackBegin)
        {
            if (player.transform.position.x > transform.position.x && facingDir == -1)
            {
                Flip();
            }
            else if (player.transform.position.x < transform.position.x && facingDir == 1)
            {
                Flip();
            }
        }
    }

    private void VisualRange()
    {
        if (!attackBegin)
        {
            if (player.transform.position.x > transform.position.x && facingDir == 1)
            {
                battleRange = 10;
            }
            else if (player.transform.position.x < transform.position.x && facingDir == -1)
            {
                battleRange = 10;
            }
            else
            {
                battleRange = 5;
            }
        }
    }


    private void Chase()
    {
        FlipController();
        if (triggerCalled)
        {
            Move(4, true);

            if (!IsGrounded() || IsHindered())
            {
                Move(0, false);
            }
        }
    }

    private void Attack()
    {
        triggerCalled = false;
        FlipController();
        Move(0, false);
        anim.SetTrigger("Attack");
    }

    private void Battle()
    {
        if (Vector2.Distance(player.transform.position, transform.position) >= attackDistance)
        {
            Chase();
        }
        else
        {
            Attack();
        }
    }

    protected override void Movement()
    {
        VisualRange();

        if (Vector2.Distance(player.transform.position, transform.position) <= (battleRange / 1))
        {
            Battle();
        }
        else
        {
            Patrol();
        }

    }

    protected override void Noise()
    {
        int noise = Random.Range(5, 9);
        lastNoiseTime = Time.time;
    }

    private void AttackBegin()
    {
        attackBegin = true;
    }

    private void AttackOver()
    {
        attackBegin = false;
    }

    protected override void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<PlayerStats>() != null)
            {
                if (hit.GetComponent<PlayerStats>().player.IsDead == false)
                {
                    if (player.IsInvincible)
                        return;

                    slash.transform.position = hit.GetComponent<PlayerStats>().transform.position - new Vector3(0,1.3f);
                    slash.anim.SetTrigger("Spark");
                    stats.DoDamage(hit.GetComponent<PlayerStats>());
                }

            }
        }

    }

}
