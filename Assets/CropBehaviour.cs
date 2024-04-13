using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CropBehaviour : MonoBehaviour
{
    //Information on what the crop will grow into
    SeedData seedToGrow;
    public Soil planted;

    [Header("Stages of life")]
    public GameObject seed;
    public GameObject seedling;
    public GameObject mature;
    public GameObject old;
    public GameObject harvestable;

    //The growth points of the crop
    int growth;
    //How many growth points it takes before it becomes harvestable
    int maxGrowth;
    public enum CropState
    {
        Seed, Seedling, Mature, Old, Harvestable
    }
    //The current stage in the crop's growth
    public CropState cropState;

    //Initialization for the crop gameobject
    //Called when the player plants a seed
    public void Plant(SeedData seedToGrow, Soil soil)
    {
        //Save the seed information
        this.seedToGrow = seedToGrow;

        //Instantiate the seedling gameobjects
        seedling = Instantiate(seedToGrow.seedling, transform);
        planted = soil;
        //Instantiate the mature gameobjects
        mature = Instantiate(seedToGrow.mature, transform);

        //Instantiate the old gameobject
        old = Instantiate(seedToGrow.old, transform);

        //Access the crop item data
        ItemData cropToYield = seedToGrow.cropToYield;

        //Intstantiate the harvestable crop
        harvestable = Instantiate(cropToYield.gameModel, transform);

        //Convert days to grow into hours
        int hoursToGrow = GameTimestamp.DaysToHours(seedToGrow.daysToGrow);
        //Convert it to minutes
        maxGrowth = GameTimestamp.HoursToMinutes(hoursToGrow);

        //Set the initial state to seed
        SwitchState(CropState.Seed);
    }

    //The crop will grow when watered
    public void Grow()
    {
        //Increase the growth poin by 1
        growth++;

        //The seed will sprout into a seedling
        if(growth >= maxGrowth *1 && cropState == CropState.Seed)
        {
            SwitchState(CropState.Seedling);
        }

        //Grow from seedling to mature
        if(growth >= maxGrowth *2 && cropState == CropState.Seedling)
        {
            SwitchState(CropState.Mature);
        }

        //Grow from mature to old
        if (growth >= maxGrowth *3 && cropState == CropState.Mature)
        {
            SwitchState(CropState.Harvestable);
        }
        gameObject.SetActive(true);
        //From old to harvestable
        //if(growth >= maxGrowth *4 && cropState == CropState.Old)
        //{
        //    SwitchState(CropState.Harvestable);
        //}

    }

    //Function to handle the state changes
    void SwitchState(CropState stateToSwitch)
    {
        //Reset everything and set all gameobjects to inactive
        seed.SetActive(false);
        seedling.SetActive(false);
        mature.SetActive(false);
        old.SetActive(false);
        harvestable.SetActive(false);

        switch (stateToSwitch)
        {
            case CropState.Seed:
                
                //Enable the seed gameobject
                seed.SetActive(true);
                break;
            case CropState.Seedling:
                //Enable the seedling gameobject
                seedling.SetActive(true);
                break;
            case CropState.Mature:
                //Enable the mature gameobject
                mature.SetActive(true);
                break;
            case CropState.Old:
                //Enable the old gameobject
                old.SetActive(true);
                    break;
            case CropState.Harvestable:
                //Enable the harvestable gameobject
                harvestable.SetActive(true);
                //Unparent it to the crop
                harvestable.transform.parent = null;
                //Debug.Log(change.Microorganism);
                if(seedToGrow.seedType == "Kentang")
                {
                    planted.gameObject.GetComponent<StatGiver>().changeMicro(5, false);
                    planted.gameObject.GetComponent<StatGiver>().changeHara(5, false);
                }
                else
                {
                    planted.gameObject.GetComponent<StatGiver>().changeMicro(10, true);
                    planted.gameObject.GetComponent<StatGiver>().changeHara(10, true);
                }
                
                
                Destroy (gameObject);
                break;
        }

        //Set the current crop state to the state we're switching to
        cropState = stateToSwitch;
    }
}