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
    public List<ItemData> craftingMaterial;

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
                    UI.transform.Find("Inven").GetChild(k).GetComponent<Image>().sprite = craftingItem[i].thumbnail;
                    k++;
                }
            }
            return true;
        }

        return false;
    }

    public void clickItem()
    {

        if(UI.transform.Find(""))


            if (craftable.Count >= 2)
        {
            Debug.Log("test1");

        }
        else
        {
            Debug.Log("test2");
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

    public void itemMovement()
    {
        foreach (ItemData item in craftingItem)
        {
            if (item.compostmaterial > 0)
            {
                craftable.Add(item);
            }
        }

        craftingMaterial = new List<ItemData>();
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
