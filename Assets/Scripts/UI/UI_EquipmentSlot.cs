using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType slotType;

    private void OnValidate()
    {
       gameObject.name = "Equipment Slot - " + slotType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if(item.data == null)
        {
            return;
        }

        Inventory.instance.AddItem(item.data as ItemData_Equipment);
        Inventory.instance.UnequipItem(item.data as ItemData_Equipment);

        ClearSlot();
        
    }
}
