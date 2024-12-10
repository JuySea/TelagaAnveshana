using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDrop : ItemDrop
{
    [SerializeField] private float chanceToLoseItem;
    public override void GenerateDrop()
    {
        Inventory inventory = Inventory.instance;
        List<InventoryItem> equipmentToRemove = new List<InventoryItem>();

        foreach (InventoryItem item in inventory.GetEqupmentList())
        {
            if(Random.Range(0,100) <= chanceToLoseItem)
            {
                DropItem(item.data);
                equipmentToRemove.Add(item);    
            }
        }

        for (int i = 0; i < equipmentToRemove.Count; i++)
        {
            inventory.UnequipItem(equipmentToRemove[i].data as ItemData_Equipment);
        }
    }
}
