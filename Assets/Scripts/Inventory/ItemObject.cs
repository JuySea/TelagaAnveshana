using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();

    private void SetupVisual()
    {
        if (itemData == null)
            return;

        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item Object -" + itemData.itemName;
    }

    public void SetupItem(ItemData item, Vector2 velocity)
    {
        itemData = item;
        rb.velocity = velocity;

        SetupVisual();
    }

    public void PickupItem()
    {
        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
