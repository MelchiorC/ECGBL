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
    public List<GameObject> craftingSlots;
    public List<GameObject> bagSlots;
    public List<GameObject> resultSlots;
    public int lastEmptyCraftingSlotId;
    public Sprite defaultSprite;
    [SerializeField]
    public ItemData compost;
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
                        Debug.Log(InventoryManager.Instance.GetInventorySlots().Length);
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
            Debug.Log("test2"+craftable.Count);
        }
    }
    public void CraftItems()
    {
        resultSlots[0].GetComponent<Image>().sprite = compost.thumbnail;
        //craftingItem.Add(new ItemData);
    }

    public void OnCraftButtonPressed()
    {
        clickItem();
        CraftItems();
    }

    public void itemMovement(bool isResultSlot, int destId, int originId, int operation, int trueDest )
    {
        Debug.Log(originId);

        foreach (ItemData item in craftingItem)
        {
            if (item.compostmaterial > 0)
            {
                craftable.Add(item);
            }
        }

        craftingMaterial = new List<ItemData>();
        //
        switch (operation)
        {
            //0 from bag to crafting slot, 1 from crafting slot back to bag, 2 from result to bag
            case 0: craftingSlots[lastEmptyCraftingSlotId].GetComponent<Image>().sprite = craftable[originId].thumbnail;
                craftingSlots[lastEmptyCraftingSlotId].GetComponent<OnItemClick>().trueDest = originId;
                bagSlots[originId].GetComponent<Image>().sprite = defaultSprite;
                    if(lastEmptyCraftingSlotId < craftingSlots.Count - 1)
                    {
                        lastEmptyCraftingSlotId++;
                    }
                   break;
            case 1:
                craftingSlots[destId].GetComponent<Image>().sprite = defaultSprite;
                bagSlots[trueDest].GetComponent<Image>().sprite = craftable[trueDest].thumbnail; ;
                if (lastEmptyCraftingSlotId > 0)
                {
                    lastEmptyCraftingSlotId--;
                }
                break;
            case 2:
                bagSlots[2].GetComponent<Image>().sprite = compost.thumbnail;
                resultSlots[0].GetComponent<Image>().sprite = defaultSprite;
                InventoryManager.Instance.ShopToInventory(new ItemSlotData(compost));
                //calculate last filledBag;
                break;
                
        }
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
