using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompostShower: MonoBehaviour
{

    public GameObject UI;
    public int[] allowedItemTypes;// input numeric code to allow the item to be put into the crafting slot. 
    //Compost maker allowed item num = 1, pesticide itemIngredient = 2. Make sure the recipe contain these item with the codes, otherwise it wont work
    public Boolean OnTrigger = false;
    public List<ItemSlotData> playerInventory;
    public List<ItemSlotData> craftable;
    public List<RecipeData> recipes;
    public List<GameObject> craftingSlots;
    public List<GameObject> bagSlots;
    public List<GameObject> resultSlots;
    public int lastEmptyCraftingSlotId;
    public Sprite defaultSprite;
    [SerializeField]
    public ItemData compost;//at current, this implementation only handle one type of output
    public GameObject draggablePrefab;
    public List<ItemData> InputedRecipeIngredients;
    public static CompostShower instance;
    List<GameObject> spawnedDraggable;
    public int k;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public Boolean CompostUI()
    {
        InputedRecipeIngredients = new List<ItemData>();
        spawnedDraggable = new List<GameObject>();
        if (OnTrigger == true)
        {
            playerInventory = new List<ItemSlotData>();
            List<ItemSlotData> temp = new List<ItemSlotData>();
            resultSlots[0].GetComponent<Image>().sprite = null;
            foreach(GameObject g in craftingSlots)
            {
                
            }
            UI.SetActive(true);
            playerInventory = InventoryManager.Instance.GetAllInventoryItems();
            temp = InventoryManager.Instance.GetAllInventoryItems();
            List<int> ints = new List<int>();
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i] != null)
                {
                    if (temp[i].itemData.compostmaterial == 0)
                    {
                        ints.Add(i);
                    }
                }
            }

            int slots = UI.transform.Find("Inven").childCount;
             k = 0;
            for (int i = 0; i < playerInventory.Count; i++)
            {
            
                if(k < UI.transform.Find("Inven").childCount)
                {
                   // UI.transform.Find("Inven").GetChild(k).GetComponent<Image>().sprite = playerInventory[i].thumbnail;
                   
                    GameObject g = Instantiate(draggablePrefab,UI.transform.Find("Inven"));
                    g.transform.position = UI.transform.Find("Inven").GetChild(k).transform.position;
                    g.GetComponent<InventorySlot>().Display(playerInventory[i]);
                    g.GetComponent<DraggableInventoryCompost>().originSnappingPosition = UI.transform.Find("Inven").GetChild(k).transform.position;
                    g.GetComponent<DraggableInventoryCompost>().compost = this;
                    spawnedDraggable.Add(g);
                    k++;
                }
              
               /* bool found = false;
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
                   
                }*/
            }
        foreach (ItemSlotData item in playerInventory)
        {
            if (item.itemData.compostmaterial > 0)
            {
                craftable.Add(item);
            }
        }
        return true;
        }

        return false;
    }
    public void HideUI()
    {
        UI.SetActive(false);
        foreach(GameObject g in spawnedDraggable)
        {
            Destroy(g);
        }
    }
    public bool isAllowed(ItemData data)
    {
        bool retValue = false;
        for(int i = 0; i < allowedItemTypes.Length; i++)
        {
            if(data.compostmaterial == allowedItemTypes[i])
            {
                retValue = true;
            }
        }
        return retValue;
    }
    public void AddItemToRecipe(ItemData data)
    {
        bool found = false;
        foreach(ItemData item in InputedRecipeIngredients)
        {
            if(item == data)
            {
                found = true;
                break;
            }
        }
        if (!found)
        {
            InputedRecipeIngredients.Add(data);
        }
       
        
    }
    public void RemoveItemToRecipe(ItemData data)
    {
        bool found = false;
        foreach (ItemData item in InputedRecipeIngredients)
        {
            if (item == data)
            {
                found = true;
                break;
            }
        }
        if (found)
        {
            InputedRecipeIngredients.Remove(data);
        }
    }
    
    public void Craft()
    {
        //spawn crafted item
        GameObject g  = Instantiate(draggablePrefab);
        g.transform.parent = UI.transform.Find("Inven");
        g.transform.position = resultSlots[0].transform.position;
        //add initial snapping position in inventory panel in this UI
        if(k < UI.transform.Find("Inven").childCount)
        {
            g.GetComponent<DraggableInventoryCompost>().originSnappingPosition = UI.transform.Find("Inven").GetChild(k).transform.position;

        }
        //super naive, exhaustive, brute force method recipe matching
        bool found = true;
        RecipeData foundR = null;
        foreach(RecipeData rd in recipes)
        {
            found = true;
            if (rd.Ingredients.Count > InputedRecipeIngredients.Count)
                continue;//this means if the player input less ingredients that supposed to, it will skip the checking
            for(int i = 0; i < InputedRecipeIngredients.Count; i++)
            {
                if (!rd.Ingredients.Contains(InputedRecipeIngredients[i])){
                    found = false;
                    break;//stop checking if the player input mismatched ingredient compared to the one currently checked.
                }
            }
            if (!found)
            {
                continue;
            }else if(found)
            {
                foundR = rd;
                break;
            }
            continue;
        }
        //Initiate the item data component;
        g.GetComponent<InventorySlot>().Display(new ItemSlotData(foundR.craftResult));
        spawnedDraggable.Add(g);
        Debug.Log(foundR);
        resultSlots[0].GetComponent<Image>().sprite = foundR.craftResult.thumbnail;
        InventoryManager.Instance.ShopToInventory(new ItemSlotData(foundR.craftResult));
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
