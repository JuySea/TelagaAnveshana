using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    private Enemy enemy;
    private ItemDrop itemDrop;

    [SerializeField] private int level;

    [Range(0f, 1f)] private float percentageModifier = .4f;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        ApplyModifiers();
        base.Start();

        enemy = GetComponent<Enemy>();
        itemDrop = GetComponent<ItemDrop>();
    }

    private void Modify(Stats stat)
    {
        for (int i = 1; i < level; i++)
        {
            float modifier = stat.GetValue() * percentageModifier;
            stat.AddModifiers(Mathf.RoundToInt(modifier));
        }
    }

    private void ApplyModifiers()
    {
        Modify(damage);
        Modify(critChance);
        Modify(critPower);

        Modify(maxHealth);
        Modify(armor);
        Modify(evasion);
        Modify(magicRes);

        Modify(fireDamage);
        Modify(iceDamage);
        Modify(lightningDamage);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        enemy.DamageEffect();
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();

        itemDrop.GenerateDrop();
    }
}
