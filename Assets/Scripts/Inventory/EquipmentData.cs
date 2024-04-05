using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment")]
public class EquipmentData : ItemData
{
    public enum ToolType
    {
        WateringCan, Shovel, Sickle
    }
    public ToolType toolType;
}
