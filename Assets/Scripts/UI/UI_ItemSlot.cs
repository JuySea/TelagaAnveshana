using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
{
    private Image itemImage;
    private TextMeshProUGUI itemText;

    public InventoryItem item;

    private void Awake()
    {
        itemImage = GetComponent<Image>();
        itemText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateSlot(InventoryItem newItem)
    {
        item = newItem;
        itemImage.color = Color.white;
        if(item != null)
        {
            itemImage.sprite = item.data.icon;

            if(item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = " ";
            }
        }
    }

    public void ClearSlot()
    {
        item = null;
        itemImage.sprite = null;
        itemImage.color = Color.clear;
        itemText.text = "";
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(item == null)
        {
            return;
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            if(item.stackSize > 1)
            {
                item.stackSize--;
            }
            else
            {
                Inventory.instance.RemoveItem(item.data);
            }

            return;
        }

        if(item.data.ItemType == ItemType.Equipment)
        {
            Inventory.instance.EquipItem(item.data);
        }
    }
}
