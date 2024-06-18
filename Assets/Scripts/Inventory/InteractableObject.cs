using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //The item information the GameObject is supposed to represent
    public ItemData item, waste;

    public virtual void Pickup()
    {
        //Set the player's inventory to the item
        InventoryManager.Instance.EquipHandSlot(item);
        InventoryManager.Instance.HandToInventory(InventorySlot.InventoryType.Item);

        InventoryManager.Instance.EquipHandSlot(waste);
        InventoryManager.Instance.HandToInventory(InventorySlot.InventoryType.Item);

        //Update the changes to the scene
        InventoryManager.Instance.RenderHand();

        //Destroy this instance so as to not have multiple copies
        Destroy(gameObject);
    }
}