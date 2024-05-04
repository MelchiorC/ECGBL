using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Seed")]
public class SeedData : ItemData
{
    //Time it takes before the seed matures into crop
    public int daysToGrow;

    //The crop the seed will yield
    public ItemData cropToYield;

    //The seedling gameobject
    public GameObject seedling;

    //The mature gameobject
    public GameObject mature;

    //The harvestable gameobject
    public GameObject harvestable;

    public ItemData waste;

    public string seedType;
}
