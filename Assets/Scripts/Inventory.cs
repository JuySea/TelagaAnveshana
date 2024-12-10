using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public List<ItemData> startingEquipment;

    [SerializeField] private List<InventoryItem> inventory;
    [SerializeField] private Dictionary<ItemData, InventoryItem> inventoryDictionary;
    [SerializeField] private Transform inventoryUI;

    [SerializeField] private List<InventoryItem> stash;
    [SerializeField] private Dictionary<ItemData, InventoryItem> stashDictionary;
    [SerializeField] private Transform stashUI;

    [SerializeField] private List<InventoryItem> equipment;
    [SerializeField] private Dictionary<ItemData_Equipment, InventoryItem> equipmentDictionary;
    [SerializeField] private Transform equipmentUI;


    private UI_ItemSlot[] inventorySlot;
    private UI_ItemSlot[] stashSlot;
    private UI_EquipmentSlot[] equipmentSlot;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    private void Start()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionary = new Dictionary<ItemData, InventoryItem>();
        inventorySlot = inventoryUI.GetComponentsInChildren<UI_ItemSlot>();

        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();
        stashSlot = stashUI.GetComponentsInChildren<UI_ItemSlot>();

        equipment = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemData_Equipment, InventoryItem>();
        equipmentSlot = equipmentUI.GetComponentsInChildren<UI_EquipmentSlot>();

        for (int i = 0; i < startingEquipment.Count; i++)
        {
            AddItem(startingEquipment[i]);
        }
    }

    private void UpdateSlotUI()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_Equipment, InventoryItem> equipment in equipmentDictionary)
            {
                if (equipment.Key.equipmentType == equipmentSlot[i].slotType)
                    equipmentSlot[i].UpdateSlot(equipment.Value);
            }
        }

        for (int i = 0; i < inventorySlot.Length; i++)
        {
            inventorySlot[i].ClearSlot();
        }

        for (int i = 0; i < stashSlot.Length; i++)
        {
            stashSlot[i].ClearSlot();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            inventorySlot[i].UpdateSlot(inventory[i]);
        }

        for (int i = 0; stash.Count > i; i++)
        {
            stashSlot[i].UpdateSlot(stash[i]);
        }
    }

    public void EquipItem(ItemData item)
    {
        ItemData_Equipment newEquipment = item as ItemData_Equipment;
        InventoryItem newItem = new InventoryItem(newEquipment);

        ItemData_Equipment oldEquipment = null;

        foreach(KeyValuePair<ItemData_Equipment,InventoryItem> equipment in equipmentDictionary)
        {
            if(equipment.Key.equipmentType == newEquipment.equipmentType)
                oldEquipment = equipment.Key; 
        }

        if(oldEquipment != null)
        {
            UnequipItem(oldEquipment);
            AddItem(oldEquipment);
        }

        equipment.Add(newItem);
        equipmentDictionary.Add(newEquipment, newItem);
        newEquipment.AddModifiers();

        RemoveItem(item);

        UpdateSlotUI();
    }

    public void UnequipItem(ItemData_Equipment oldEquipment)
    {
        if (equipmentDictionary.TryGetValue(oldEquipment, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDictionary.Remove(oldEquipment);
            oldEquipment.RemoveModifiers();

            for(int i = 0; i < equipmentSlot.Length; i++)
            {
                if (equipmentSlot[i].slotType == oldEquipment.equipmentType)
                {
                    equipmentSlot[i].ClearSlot();
                }
            }
        }
    }

    public void AddItem(ItemData item)
    {
        if (item.ItemType == ItemType.Item)
            AddToInventory(item);
        else if (item.ItemType == ItemType.Equipment)
            AddToStash(item);

        UpdateSlotUI();
    }

    private void AddToInventory(ItemData item)
    {
        if (inventoryDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            inventory.Add(newItem);
            inventoryDictionary.Add(item, newItem);
        }
    }

    private void AddToStash(ItemData item)
    {
        if (stashDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            stash.Add(newItem);
            stashDictionary.Add(item, newItem);
        }
    }

    public void RemoveItem(ItemData item)
    {
        if(inventoryDictionary.TryGetValue(item, out InventoryItem itemValue))
        {
            if (itemValue.stackSize <= 1)
            {
                inventory.Remove(itemValue);
                inventoryDictionary.Remove(item);
            }
            else
            {
                itemValue.RemoveStack();
            }
        }

        if(stashDictionary.TryGetValue(item, out InventoryItem stashValue))
        {
            if(stashValue.stackSize <= 1)
            {
                stash.Remove(stashValue);
                stashDictionary.Remove(item);
            }
            else
            {
                stashValue.RemoveStack();
            }
        }

        UpdateSlotUI();
    }

    public bool CanCraft(ItemData_Equipment itemToCraft, List<InventoryItem> requiredMaterials)
    {
        List<InventoryItem> materialToRemove = new List<InventoryItem>();

        for (int i = 0; i < requiredMaterials.Count; i++) 
        {
            if (stashDictionary.TryGetValue(requiredMaterials[i].data, out InventoryItem value))
            {
                if(value.stackSize < requiredMaterials[i].stackSize)
                {
                    Debug.Log("Not enough materials");
                    return false;
                }
                else
                {
                    materialToRemove.Add(value);
                }
            }
            else
            {
                Debug.Log("Not enough materials");
                return false;
            }
        }

        for (int j = 0; j < materialToRemove.Count; j++)
        {
            RemoveItem(materialToRemove[j].data);
        }

        AddItem(itemToCraft);
        return true;
    }

    public ItemData_Equipment GetEquipment(EquipmentType type)
    {
        ItemData_Equipment equipedItem = null;

        foreach (KeyValuePair<ItemData_Equipment,InventoryItem> item in equipmentDictionary)
        {
            if(item.Key.equipmentType == type)
            {
                equipedItem = item.Key;
            }
        }

        return equipedItem;
    }

    public List<InventoryItem> GetEqupmentList() => equipment;
}
