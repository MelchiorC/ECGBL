using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompostShower : MonoBehaviour
{
    public GameObject UI;
    public Boolean OnTrigger = false;
    public List<ItemData> craftingItem;
    public List<ItemData> craftable;

    public Boolean CompostUI()
    {
        if (OnTrigger == true)
        {
            craftingItem = new List<ItemData>();
            List<ItemData> temp = new List<ItemData>();
            UI.SetActive(true);
            if (InventoryManager.Instance.GetEquippedSlotItem() != null)
            {
                craftingItem.Add(InventoryManager.Instance.GetEquippedSlotItem());
                temp.Add(InventoryManager.Instance.GetEquippedSlotItem());
            }
            if (InventoryManager.Instance.GetInventorySlots().Length > 0)
            {
                for (int i = 0; i < InventoryManager.Instance.GetInventorySlots().Length; i++)
                {
                    if (InventoryManager.Instance.GetInventorySlots()[i].itemData != null)
                    {
                        craftingItem.Add(InventoryManager.Instance.GetInventorySlots()[i].itemData);
                        temp.Add(InventoryManager.Instance.GetInventorySlots()[i].itemData);
                    }
                }
            }
            List<int> ints = new List<int>();
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i] != null)
                {
                    if (temp[i].compostmaterial == 0)
                    {
                        ints.Add(i);
                    }
                }
            }

            int slots = UI.transform.Find("Inven").childCount;
            int k = 0;
            for (int i = 0; i < craftingItem.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < ints.Count; j++)
                {
                    if (i == ints[j])
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    UI.transform.Find("Inven").GetChild(i).GetComponent<Image>().sprite = craftingItem[i].thumbnail;
                    Debug.Log(i);
                }
            }
            return true;
        }

        return false;
    }

    public void clickItem()
    {
        craftable = new List<ItemData>();

        foreach (ItemData item in craftingItem)
        {
            if (item.compostmaterial > 0)
            {
                Debug.Log("test");
                craftable.Add(item);
            }
        }

        // Assuming crafting logic involves combining compostable items
        // Placeholder logic for crafting
        if (craftable.Count >= 2)
        {
            Debug.Log("Two or more compostable items found. Crafting possible.");
            // Implement your crafting logic here
        }
        else
        {
            Debug.Log("Not enough compostable items to craft.");
        }
    }
    public void CraftItems()
    {
        
    }

    public void OnCraftButtonPressed()
    {
        clickItem();
        CraftItems();
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTrigger = true;
    }
    private void OnTriggerExit(Collider other)
    {
        OnTrigger = false;
    }
}
