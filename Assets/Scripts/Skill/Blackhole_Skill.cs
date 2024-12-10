using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Skill : Skill
{
    [SerializeField] private GameObject blackholePrefab;
    public float maxSize;
    public float growSpeed;
    public float shrinkSpeed;
    public int attackAmounts;
    public float attackCooldown;
    public float blackholeDuration;

    private Blackhole_Skill_Controller currentBlackhole;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();

    }

    public override void UseSkill()
    {
        base.UseSkill();

        Debug.Log("USE");
        GameObject blackHole = Instantiate(blackholePrefab, player.transform.position, Quaternion.identity);
        currentBlackhole = blackHole.GetComponent<Blackhole_Skill_Controller>();
        currentBlackhole.SetupBlackhole(maxSize, growSpeed, shrinkSpeed, attackAmounts, attackCooldown, blackholeDuration);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool BlackholeFinished()
    {
        if (!currentBlackhole)
            return false;
            
        

        if (currentBlackhole.playerCanExitState)
        {
            currentBlackhole = null;
            return true;
        }

        return false;
    }
}
