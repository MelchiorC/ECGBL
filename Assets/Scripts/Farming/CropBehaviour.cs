using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class CropBehaviour : MonoBehaviour
{
    //Information on what the crop will grow into
    public SeedData seedToGrow;
    SeedData waste;
    private Soil planted;

    [Header("Stages of life")]
    public GameObject seed;
    public GameObject seedling;
    public GameObject mature;
    public GameObject mature2;
    public GameObject harvestable;

    //The growth points of the crop
    public int growth;
    //How many growth points it takes before it becomes harvestable
    int maxGrowth;
    public enum CropState
    {
        Seed, Seedling, Mature, Mature2, Harvestable
    }
    //The current stage in the crop's growth
    public CropState cropState;
    public bool diseased;
    List<Color[]> materialsColors;
    //Initialization for the crop gameobject
    //Called when the player plants a seed
    private void Start()
    {
        if (planted == null)
        {
            Debug.Log("planted");
        }
        //storing colors references 
        materialsColors = new List<Color[]>();
        Debug.Log(seedling.GetComponent<Renderer>().materials);
        Color[] k = new Color[seedling.GetComponent<Renderer>().materials.Length];
        for(int i = 0; i < seedling.GetComponent<Renderer>().materials.Length; i++)
        {
            k[i] = seedling.GetComponent<Renderer>().materials[i].color;
        }
        materialsColors.Add(k);
        ///
        k = new Color[mature.GetComponent<Renderer>().materials.Length];
        for (int i = 0; i < mature.GetComponent<Renderer>().materials.Length; i++)
        {
            k[i] = mature.GetComponent<Renderer>().materials[i].color;
        }
        materialsColors.Add(k);
        //
        k = new Color[mature2.GetComponent<Renderer>().materials.Length];
        for (int i = 0; i < mature2.GetComponent<Renderer>().materials.Length; i++)
        {
            k[i] = mature2.GetComponent<Renderer>().materials[i].color;
        }
        materialsColors.Add(k);
        //
        k = new Color[harvestable.GetComponent<Renderer>().materials.Length];
        for (int i = 0; i < harvestable.GetComponent<Renderer>().materials.Length; i++)
        {
            k[i] = harvestable.GetComponent<Renderer>().materials[i].color;
        }
        materialsColors.Add(k);

     
        //  materialsColors[1] = ;
        //  materialsColors[2] = ;
        //  materialsColors[3] = ;

    }
    public void Plant(SeedData seedToGrow, Soil soil)
    {
        //Save the seed information
        this.seedToGrow = seedToGrow;

        //Instantiate the seedling gameobjects
        seedling = Instantiate(seedToGrow.seedling, transform);
        planted = soil;
        //Instantiate the mature gameobjects
        mature = Instantiate(seedToGrow.mature, transform);
        mature2 = Instantiate(seedToGrow.mature2, transform);

        //Access the crop item data
        ItemData cropToYield = seedToGrow.cropToYield;
        ItemData waste = seedToGrow.waste;

        //Intstantiate the harvestable crop
        harvestable = Instantiate(seedToGrow.harvestable, transform);

        //Convert days to grow into hours
        int hoursToGrow = GameTimestamp.DaysToHours(seedToGrow.daysToGrow);
        //Convert it to minutes
        maxGrowth = seedToGrow.daysToGrow;

        //Set the initial state to seed
        SwitchState(CropState.Seed);
    }
    public void RevertDiseaseState()
    {
        diseased = false;

         for (int i = 0;i< seedling.GetComponent<Renderer>().materials.Length;i++)
        {
            seedling.GetComponent<Renderer>().materials[i].SetColor("_BaseColor", materialsColors[0][i]);
        }
        for (int i = 0; i < mature.GetComponent<Renderer>().materials.Length; i++)
        {
            mature.GetComponent<Renderer>().materials[i].SetColor("_BaseColor", materialsColors[1][i]);
        }
        for (int i = 0; i < mature2.GetComponent<Renderer>().materials.Length; i++)
        {
            mature2.GetComponent<Renderer>().materials[i].SetColor("_BaseColor", materialsColors[2][i]);
        }
        for (int i = 0; i < harvestable.GetComponent<Renderer>().materials.Length; i++)
        {
            harvestable.GetComponent<Renderer>().materials[i].SetColor("_BaseColor", materialsColors[3][i]);
        }
    }
    //The crop will grow when watered
    public void Grow()
    {
        //Increase the growth poin by 1
        growth++;

        //The seed will sprout into a seedling
        if (growth >= maxGrowth * 1 && cropState == CropState.Seed)
        {
            SwitchState(CropState.Seedling);
        }

        //Grow from seedling to mature
        if (growth >= maxGrowth * 2 && cropState == CropState.Seedling)
        {
            SwitchState(CropState.Mature);
        }

        //Grow from mature to mature2
        if (growth >= maxGrowth * 3 && cropState == CropState.Mature)
        {
            SwitchState(CropState.Mature2);
        }

        //Grow from mature to harvestable
        if (growth >= maxGrowth * 4 && cropState == CropState.Mature2)
        {
            SwitchState(CropState.Harvestable);
        }
        gameObject.SetActive(true);
    }

    //Function to handle the state changes
    void SwitchState(CropState stateToSwitch)
    {
        //Reset everything and set all gameobjects to inactive
        seed.SetActive(false);
        seedling.SetActive(false);
        mature.SetActive(false);
        mature2.SetActive(false);
        harvestable.SetActive(false);

        switch (stateToSwitch)
        {
            case CropState.Seed:
                //Enable the seed gameobject
                seed.SetActive(true);
               // if(diseased)
                //{
                   // Material[] materials = seed.GetComponent<Renderer>().materials;
                   // foreach(Material m in materials)
                   // {
                     //   m.SetColor ("_BaseColor", new Color(110, 38, 14));
                    //}
               // }
                break;

            case CropState.Seedling:
                //Enable the seedling gameobject
                seedling.SetActive(true);
                if (diseased)
                {
                    Material[] materials = seedling.GetComponent<Renderer>().materials;
                    foreach (Material m in materials)
                    {
                        m.SetColor("_BaseColor", new Color(0.212f, 0.063f, 0.004f));
                    }
                }
                break;

            case CropState.Mature:
                //Enable the mature gameobject
                planted.status.Compost = false;
                mature.SetActive(true);
                if (diseased)
                {
                    Material[] materials = mature.GetComponent<Renderer>().materials;
                    foreach (Material m in materials)
                    {
                        m.SetColor("_BaseColor", new Color(0.212f, 0.063f, 0.004f));
                    }
                }
                break;

            case CropState.Mature2:
                //Enable the mature2 gameobjects
                mature2.SetActive(true);
                if (diseased)
                {
                    Material[] materials = mature2.GetComponent<Renderer>().materials;
                    foreach (Material m in materials)
                    {
                        m.SetColor("_BaseColor", new Color(0.212f, 0.063f, 0.004f));
                    }
                }
                break;

            case CropState.Harvestable:
                harvestable.GetComponent<InteractableObject>().boost += planted.status.TotalPupuk;
                if (planted.status.LandRotation)
                {
                    harvestable.GetComponent<InteractableObject>().boost += 1;
                }
                if (diseased)
                {
                    Material[] materials = harvestable.GetComponent<Renderer>().materials;
                    foreach (Material m in materials)
                    {
                        m.SetColor("_BaseColor", new Color(0.212f, 0.063f, 0.004f));
                    }
                }
                //Enable the harvestable gameobject
                planted.status.TotalPupuk = 0;
                planted.status.Compost = false;
                harvestable.SetActive(true);
                harvestable.GetComponent<InteractableObject>();
                //Unparent it to the crop
                harvestable.transform.parent = null;
                if (seedToGrow.seedType == "Legume")
                {
                    planted.status.LandRotation = true;
                }
                else
                {
                    planted.status.LandRotation = false;
                }
                break;
        }

        //Set the current crop state to the state we're switching to
        cropState = stateToSwitch;
    }
    public void DestroyCrops()
    {
        Destroy(seedling);
        Destroy(seed);
        Destroy(mature);
        Destroy(mature2);
        Destroy(harvestable);
    }
}