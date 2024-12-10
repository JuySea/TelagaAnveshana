using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem 
{

    public ItemData data;
    public int stackSize;

    public InventoryItem(ItemData item)
    {
        this.data = item;
        AddStack();
    }

    public void AddStack() => stackSize++;
    public void RemoveStack() => stackSize--;
}
