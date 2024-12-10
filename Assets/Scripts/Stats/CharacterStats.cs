using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private EntityFX fx;

    [Header("Major Stats")]
    public Stats strength; // increase damage by 1 and crit power by 1%
    public Stats agility; // increase evasion by 1% and crit chance by 1%
    public Stats intelligence; // increase magic damage by 1 and magic resistance by 3
    public Stats vitality; // increase health by 5

    [Header("Offensive Stats")]
    public Stats damage;
    public Stats critChance;
    public Stats critPower;

    [Header("Defensive Stats")]
    public Stats maxHealth;
    public Stats armor;
    public Stats evasion;
    public Stats magicRes;

    [Header("Magic Stats")]
    public Stats fireDamage;
    public Stats iceDamage;
    public Stats lightningDamage;

    public bool isIgnited;
    public bool isChilled;
    public bool isShocked;

    private float ailmentDuration = 4f;

    private float ignitedTimer; 
    private float ignitedDamageTimer; 
    [SerializeField] private float ignitedDamageCooldown;
    [SerializeField] private int igniteDamage;

    private float chilledTimer;
    private float chilledDamageTimer;
    [SerializeField] private float chilledDamageCooldown;
    [SerializeField] private int chillDamage;

    private float shockedTimer;
    private float shockedDamageTimer;
    [SerializeField] private float shockedDamageCooldown;
    [SerializeField] private int shockedDamage;

    public int currentHealth;
    public System.Action onHealthChanged;
    public bool isDead { get; private set; }

    protected virtual void Awake()
    {
        fx = GetComponentInChildren<EntityFX>();
    }

    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = GetMaxHealthValue();
    }

    protected virtual void Update()
    {
        IgniteDamage();
        ChillDamage();
    }

    private void IgniteDamage()
    {
        ignitedTimer -= Time.deltaTime;
        ignitedDamageTimer -= Time.deltaTime;

        if (ignitedTimer < 0)
            isIgnited = false;

        if (ignitedDamageTimer <= 0 && isIgnited)
        {
            DecreaseHealthBy(igniteDamage);
            ignitedDamageTimer = ignitedDamageCooldown;

            //if (currentHealth < 0)
            //{
            //    Die();
            //}
        }
    }

    private void ChillDamage()
    {
        chilledTimer -= Time.deltaTime;
        chilledDamageTimer -= Time.deltaTime;

        if (chilledTimer < 0)
            isChilled = false;

        if (chilledDamageTimer <= 0 && isChilled)
        {
            DecreaseHealthBy(chillDamage);
            chilledDamageTimer = chilledDamageCooldown;

            //if (currentHealth < 0)
            //{
            //    Die();
            //}
        }
    }

    private void ShockDamage()
    {
        shockedTimer -= Time.deltaTime;
        shockedDamageTimer -= Time.deltaTime;

        if (shockedTimer < 0)
            isShocked = false;

        if (shockedDamageTimer <= 0 && isShocked)
        {
            DecreaseHealthBy(chillDamage);
            shockedDamageTimer = shockedDamageCooldown;

            //if (currentHealth < 0)
            //{
            //    Die();
            //}
        }
    }

    public virtual void DoDamage(CharacterStats target)
    {
        if (TargetCanAvoidAttack(target))
            return;

        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }


        totalDamage = CheckTargetArmor(target, totalDamage);

        target.TakeDamage(totalDamage);
    }

    public virtual void DoMagicalDamage(CharacterStats target)
    {
        int fireDamage = this.fireDamage.GetValue();
        int iceDamage = this.iceDamage.GetValue();
        int lightningDamage = this.lightningDamage.GetValue();

        int totalMagicalDamage = fireDamage + iceDamage + lightningDamage + intelligence.GetValue();
        totalMagicalDamage -= target.magicRes.GetValue() + (target.intelligence.GetValue() * 3);
        totalMagicalDamage = Mathf.Clamp(totalMagicalDamage, 0, int.MaxValue);

        target.TakeDamage(totalMagicalDamage);

        bool canApplyIgnite = fireDamage > iceDamage & fireDamage > lightningDamage;
        bool canApplyChill = iceDamage > fireDamage & iceDamage > lightningDamage;
        bool canApplyShock = lightningDamage > fireDamage & lightningDamage > iceDamage;

        if (Mathf.Max(fireDamage, iceDamage, lightningDamage) <= 0)
            return;

        while(!canApplyIgnite && !canApplyChill && !canApplyShock)
        {
            if(Random.value < .5f && fireDamage > 0)
            {
                canApplyIgnite = true;
                target.ApplyAilment(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
            if(Random.value < .5f && iceDamage > 0)
            {
                canApplyChill = true;
                target.ApplyAilment(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
            if(Random.value < .5f && lightningDamage > 0)
            {
                canApplyShock = true;
                target.ApplyAilment(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
        }

        if (canApplyIgnite)
        {
            SetupIgniteDamage(Mathf.RoundToInt(fireDamage * .2f));
        }

        target.ApplyAilment(canApplyIgnite, canApplyChill, canApplyShock);


    }

    public void ApplyAilment(bool ignite, bool chilled, bool shocked)
    {
        if (isIgnited || isChilled || isShocked)
            return;

        if (ignite)
        {
            isIgnited = ignite;
            ignitedTimer = ailmentDuration;

            fx.IgnitedFX(ailmentDuration);
        }

        if(chilled)
        {
            isChilled = chilled;
            chilledTimer = ailmentDuration;

            float slowPercentage = .2f;
            GetComponent<Entity>().SlowEntity(slowPercentage, ailmentDuration);
            fx.ChillFX(ailmentDuration);
        }

        if (shocked)
        {
            isShocked = shocked;
            shockedTimer = ailmentDuration;

            fx.ShockFX(ailmentDuration);
        }


        isChilled = chilled;
        isShocked = shocked;
    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + (vitality.GetValue() * 5);
    }

    private int CheckTargetArmor(CharacterStats target, int totalDamage)
    {
        if(target.isChilled)
        {
            totalDamage -= Mathf.RoundToInt(target.armor.GetValue() * .8f);
        }
        else
        {
            totalDamage -= target.armor.GetValue();
        }

        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    protected virtual void DecreaseHealthBy(int damage)
    {
        currentHealth -= damage;

        if (onHealthChanged != null)
            onHealthChanged();
    }
    public virtual void TakeDamage(int damage)
    {
        DecreaseHealthBy(damage);

        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public void SetupIgniteDamage(int damage) => igniteDamage = damage;
    private bool TargetCanAvoidAttack(CharacterStats target)
    {
        int totalEvasion = target.evasion.GetValue() + target.agility.GetValue();

        if (isShocked)
            totalEvasion += 20;

        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }

        return false;
    }
    private bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue() + agility.GetValue();
        if(Random.Range(0, 100) <= totalCriticalChance)
        {
            return true;
        }

        return false;

    }

    private int CalculateCriticalDamage(int damage)
    {
        float totalCritPower = (critPower.GetValue() + strength.GetValue()) * 0.1f;
        float critDamage = damage + totalCritPower;
        return Mathf.RoundToInt(critDamage);
    }
    protected virtual void Die()
    {
        isDead = true;
    }
}
