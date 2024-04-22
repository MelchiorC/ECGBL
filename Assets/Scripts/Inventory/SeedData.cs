using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Seed")]
public class SeedData : ItemData
{
    //Time it takes before the seed matures into crop
    public int daysToGrow;

    //The crop the eed will yield
    public ItemData cropToYield;

    //The seedling gameobject
    public GameObject seedling;

    //The mature gameobject
    public GameObject mature;

    public GameObject harvestable;

    public string seedType;
}
