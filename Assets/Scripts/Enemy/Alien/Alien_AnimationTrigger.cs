using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien_AnimationTrigger : MonoBehaviour
{
    private Enemy_Alien alien => GetComponentInParent<Enemy_Alien>();
    
    private void AnimationFinishTrigger()
    {
        alien.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(alien.attackCheck.position, alien.attackCheckRadius);

        foreach(var hit in colliders)
        {
            if(hit.GetComponent<Player>() != null)
            {
                PlayerStats player = hit.GetComponent<PlayerStats>();
                alien.stats.DoDamage(player);
            }
        }
    }

    private void DiedTrigger()
    {
        alien.Dissolve();
    }

    private void OpenCounterWindow() => alien.OpenCounterAttackWindow();
    private void CloseCounterWindow() => alien.CloseCounterAttackWindow();

}
