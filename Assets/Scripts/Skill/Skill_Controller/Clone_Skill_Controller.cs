using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill_Controller : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;
    private float cloneTimer;
    private Transform closestEnemy;

    [SerializeField] private Transform attackCheck;
    private float attackCheckRadius = .8f;

    private void Awake()
    {
        anim = GetComponent<Animator>();    
        sr = GetComponent<SpriteRenderer>();
        attackCheck = GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if(cloneTimer < 0)
        {
            sr.color = new Color(1,1,1, sr.color.a - (Time.deltaTime * 1));
        }

        if(sr.color.a < 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetupClone(Transform clonePosition, float cloneDuration, bool canAttack, Vector3 offset)
    {
        if(canAttack) 
            anim.SetInteger("AttackVariant", Random.Range(1, 3));
        FaceClosestTarget();
        transform.position = clonePosition.position + offset;
        cloneTimer = cloneDuration;
    }

    private void AnimationTrigger() => cloneTimer = -1;

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                hit.GetComponent<Enemy>().DamageEffect();
            }
        }
    }

    private void FaceClosestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);
        float closestDistance = Mathf.Infinity;

        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if(distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform; 
                }
            }

            if(closestEnemy != null)
            {
                if(transform.position.x > closestEnemy.position.x)
                {
                    transform.Rotate(0, 180, 0);
                }
            }
        }
    }
}
