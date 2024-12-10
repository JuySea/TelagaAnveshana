using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Armor,
    Amulet,
    Flask
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_Equipment : ItemData
{
    public EquipmentType equipmentType;
    public ItemEffect[] itemEffects;

    [Header("Major Stats")]
    public int strength; // increase damage by 1 and crit power by 1%
    public int agility; // increase evasion by 1% and crit chance by 1%
    public int intelligence; // increase magic damage by 1 and magic resistance by 3
    public int vitality; // increase health by 5

    [Header("Offensive Stats")]
    public int damage;
    public int critChance;
    public int critPower;

    [Header("Defensive Stats")]
    public int maxHealth;
    public int armor;
    public int evasion;
    public int magicRes;

    [Header("Magic Stats")]
    public int fireDamage;
    public int iceDamage;
    public int lightningDamage;

    [Header("Crafting Requirements")]
    public List<InventoryItem> craftingMaterials;

    public void AddModifiers()
    {
        PlayerStats player = PlayerManager.instance.player.GetComponent<PlayerStats>();

        player.strength.AddModifiers(strength);
        player.agility.AddModifiers(agility);
        player.intelligence.AddModifiers(intelligence);
        player.vitality.AddModifiers(vitality);

        player.damage.AddModifiers(damage);
        player.critChance.AddModifiers(critChance);
        player.critPower.AddModifiers(critPower);

        player.maxHealth.AddModifiers(maxHealth);
        player.armor.AddModifiers(armor);
        player.evasion.AddModifiers(evasion);
        player.magicRes.AddModifiers(magicRes);

        player.fireDamage.AddModifiers(fireDamage);
        player.iceDamage.AddModifiers(iceDamage);
        player.lightningDamage.AddModifiers(lightningDamage);
    }

    public void RemoveModifiers()
    {
        PlayerStats player = PlayerManager.instance.player.GetComponent<PlayerStats>();

        player.strength.RemoveModifiers(strength);
        player.agility.RemoveModifiers(agility);
        player.intelligence.RemoveModifiers(intelligence);
        player.vitality.RemoveModifiers(vitality);

        player.damage.RemoveModifiers(damage);
        player.critChance.RemoveModifiers(critChance);
        player.critPower.RemoveModifiers(critPower);

        player.maxHealth.RemoveModifiers(maxHealth);
        player.armor.RemoveModifiers(armor);
        player.evasion.RemoveModifiers(evasion);
        player.magicRes.RemoveModifiers(magicRes);

        player.fireDamage.RemoveModifiers(fireDamage);
        player.iceDamage.RemoveModifiers(iceDamage);
        player.lightningDamage.RemoveModifiers(lightningDamage);
    }

    public void ExecuteEffect()
    {
        foreach (var effect in itemEffects)
        {
            effect.ExecuteEffect();
        }
    }

}
