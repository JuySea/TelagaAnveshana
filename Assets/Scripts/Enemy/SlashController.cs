using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : MonoBehaviour
{
    public Animator anim {  get; private set; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
