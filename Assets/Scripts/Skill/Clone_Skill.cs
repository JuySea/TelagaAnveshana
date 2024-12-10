using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [SerializeField] private GameObject clonePrefab;
    public bool canAttack;

    public void CreateClone(Transform clonePosition, float cloneDuration, Vector3 offset)
    {
        GameObject clone = Instantiate(clonePrefab);
        clone.GetComponent<Clone_Skill_Controller>().SetupClone(clonePosition, cloneDuration, canAttack, offset);
    }
}
