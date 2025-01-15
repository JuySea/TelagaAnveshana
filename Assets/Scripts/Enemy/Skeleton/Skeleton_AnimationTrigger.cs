using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton_AnimationTrigger : MonoBehaviour
{
    private Enemy_Skeleton skeleton => GetComponentInParent<Enemy_Skeleton>();
    
    private void AnimationFinishTrigger()
    {
        skeleton.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(skeleton.attackCheck.position, skeleton.attackCheckRadius);

        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Player>() != null)
            {
                PlayerStats player = hit.GetComponent<PlayerStats>();
                skeleton.stats.DoDamage(player);
            }
        }
    }

    private void OpenCounterWindow() => skeleton.OpenCounterAttackWindow();
    private void CloseCounterWindow() => skeleton.CloseCounterAttackWindow();

}
