using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafMechanism : MonoBehaviour
{
    public ItemData Leaf;
    // Initialize timestamp for leaf collection
    public GameTimestamp FirstDay;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GatherLeaf();
        }

        DisplayLeafCollectionPrompt();
    }

    private void GatherLeaf()
    {
        // Ensure timestamp comparison and item collection conditions
        if (FirstDay == null || GameTimestamp.CompareTimestamps(FirstDay, TimeManager.Instance.GetGameTimestamp()) >= 1)
        {
            FirstDay = TimeManager.Instance.GetGameTimestamp();
            InventoryManager.Instance.EquipHandSlot(Leaf);
            InventoryManager.Instance.HandToInventory(InventorySlot.InventoryType.Item);
        }
    }

    private void DisplayLeafCollectionPrompt()
    {
        Debug.Log("Press 'E' to gather the leaf.");
    }
}
