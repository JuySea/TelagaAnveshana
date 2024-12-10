using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Blackhole_Hotkey_Controller : MonoBehaviour
{
    private SpriteRenderer sr;
    private KeyCode keyCode;
    private TextMeshProUGUI text;
    private Transform enemy;
    private Blackhole_Skill_Controller blackhole;
    public void SetupHotkey(KeyCode keyCode, Transform enemy, Blackhole_Skill_Controller blackhole)
    {
        sr = GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        this.enemy = enemy;
        this.blackhole = blackhole;

        this.keyCode = keyCode;
        text.text = this.keyCode.ToString();        
    }

    void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
            blackhole.AddEnemyToTarget(enemy);

            text.color = Color.clear;
            sr.color = Color.clear;
        }
    }
}
