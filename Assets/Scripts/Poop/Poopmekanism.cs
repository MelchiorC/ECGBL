using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poopmekanism : MonoBehaviour
{
    public ItemData Poop;
    // Allow dung collection by default
    public GameTimestamp Hari1;
    void Update()
    {
        // Check if dirt is available and player presses the 'E' key
        if ( Input.GetKeyDown(KeyCode.E))
        {
            CollectPoop();
        }
        
        // You can remove or update this line if you want to change how dirt becomes available
        ShowPoopCollectionPrompt();
    }

    private void CollectPoop()
    {

        if(Hari1 == null || GameTimestamp.CompareTimestamps(Hari1, TimeManager.Instance.GetGameTimestamp() ) >= 1)
        {
            Hari1 = TimeManager.Instance.GetGameTimestamp();
            InventoryManager.Instance.EquipHandSlot(Poop);
            InventoryManager.Instance.HandToInventory(InventorySlot.InventoryType.Item); 
        }
       
    }

    private void ShowPoopCollectionPrompt()
    {
        Debug.Log("Press 'E' to collect dirt.");
    }
}