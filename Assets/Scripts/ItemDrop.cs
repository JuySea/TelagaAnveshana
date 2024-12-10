using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int itemAmount;
    [SerializeField] private ItemData[] possibleDrop;
    [SerializeField] private List<ItemData> dropList;

    [SerializeField] private GameObject itemPrefab;

    private void Awake()
    {
        dropList = new List<ItemData>();
    }

    public virtual void GenerateDrop()
    {
        for (int i = 0; i < possibleDrop.Length; i++)
        {
            if (Random.Range(0, 100) <= possibleDrop[i].dropChance)
            {
                dropList.Add(possibleDrop[i]);
            }
        }

        for (int i = 0; i < itemAmount; i++)
        {
            if(dropList.Count <= 0)
            {
                return;
            }

            ItemData randomItem = dropList[Random.Range(0, dropList.Count - 1)];
            dropList.Remove(randomItem);
            DropItem(randomItem);
        }
    }


    protected void DropItem(ItemData item)
    {
        GameObject itemDrop = Instantiate(itemPrefab, transform.position, Quaternion.identity);
        Vector2 randomVelocity = new Vector2(Random.Range(-5,5), Random.Range(15,20));

        itemDrop.GetComponent<ItemObject>().SetupItem(item, randomVelocity);
    }
}
