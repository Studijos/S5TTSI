using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;

    public Button RemoveButton;

    public Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    public void RemoveItem()
    {
        InventoryManager.Instance.Remove(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Medkit:
                    Player.Instance.IncreaseHealth(item.value);

                break;
            case Item.ItemType.Ammo:
                ProjectileGun.Instance.IncreaseAmmo(item.value);
                break;
            default:
                break;
        }
        Debug.Log("Used");
        RemoveItem();
    }
}
