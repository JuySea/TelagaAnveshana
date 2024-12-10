using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public Clone_Skill clone;
    public Sword_Skill sword;
    public Blackhole_Skill blackhole;

    private void Awake()
    {
        clone = GetComponent<Clone_Skill>();
        sword = GetComponent<Sword_Skill>();
        blackhole = GetComponent<Blackhole_Skill>();
    }
    void Start()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        } 
        else
        {
            instance = this;
        }
       
    }

}
