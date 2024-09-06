using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Rendering;
using UnityEngine;

public class Soil : MonoBehaviour, ITimeTracker
{
    public enum LandStatus
    {
        Soil, Compost,Curved,CurvedCompost,Watered, Growing, Harvested, Default, Stick
    }
    
    public LandStatus landStatus;
    public Stat status;
    public UIHara Hara;

    public Material DrySoilMat, WetSoilMat, GrowingMat, HarvestedMat, DefaultMat;
    public Material UsableUV;

    new Renderer renderer;
    private MeshFilter filter;

    public Mesh Default, Compost, Curved,CurvedCompost;

    //The selection gameobject to enable when the player is selecting the land
    public GameObject select;

    //Cache the time the land was watered
    [SerializeField]
    public GameTimestamp timeWatered;
    [SerializeField]
    public GameTimestamp timeWatered2;



    [Header("Crops")]
    //The crop prefab to instantiate
    public GameObject cropPrefab;

    //The crop currently planted on the land
    CropBehaviour cropPlanted = null;
   
    // Start is called before the first frame update

    void Start()
    {
        filter = GetComponent<MeshFilter>();

        //Get renderer component
        renderer = GetComponent<Renderer>();

        //Set the land to soil by default
        SwitchLandStatus(LandStatus.Default);

        //Deselect the land by default
        Select(false);

        //Add this to TimeManager's listener list
        TimeManager.Instance.RegisterTracker(this);
    }
    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        //Set land status accordingly
        landStatus = statusToSwitch;
        
        Material materialToSwitch = DrySoilMat;
       
        //Declare what material to switch to
        switch (statusToSwitch)
        {
            case LandStatus.Soil:
                //Switch to the soil material
                materialToSwitch = DrySoilMat;
                changeToDefault();
                break;

            case LandStatus.Compost:
                //Switch mesh and material to compost
                materialToSwitch = UsableUV;
                if(filter.mesh.name == "Cube.011 Instance") filter.mesh = CurvedCompost;
                else filter.mesh = Compost;
                break;

            case LandStatus.Curved:
                materialToSwitch = UsableUV;
                filter.mesh = Curved;
                break;

            case LandStatus.CurvedCompost:
                materialToSwitch = UsableUV;
                filter.mesh = CurvedCompost;
                break;

            case LandStatus.Watered:
                //Switch to Watered material
                materialToSwitch = WetSoilMat;
                //Cache the time it was watered
                timeWatered = TimeManager.Instance.GetGameTimestamp();
                break;

            case LandStatus.Growing:
                //Switch to Growing material
                materialToSwitch = GrowingMat;
                break;

            case LandStatus.Harvested:
                //Switch to Harvested Material 
                materialToSwitch = HarvestedMat;
                break;

            case LandStatus.Default:
                //Switch to Default Material & Mesh
                materialToSwitch = DefaultMat;
                filter.mesh = Default;
                break;
        }
   
        //Get the renderer to apply change
        renderer.material = materialToSwitch;
    }

    public void Select(bool toggle)
    {
        select.SetActive(toggle);
    }

    public void changeToCompost()
    {
        filter.mesh = Compost;
    }

    public void changeToCurved()
    {
        filter.mesh = Curved;
    }

    public void changeToCurvedCompost() 
    {
        filter.mesh = CurvedCompost;
    }

    public void changeToDefault()
    {
        filter.mesh = Default;
    }

    //When the player presses the interact button while selecting this land
    public void Interact()
    {
        //Check the player's tool slot
        ItemData toolSlot = InventoryManager.Instance.GetEquippedSlotItem(InventorySlot.InventoryType.Tool);
        //If there's nothing equipped, return
        if(!InventoryManager.Instance.SlotEquipped(InventorySlot.InventoryType.Tool))
        {
            return;
        }

        //Try casting the itemdata in the toolslot as equipmentdata
        EquipmentData equipmentTool = toolSlot as EquipmentData;

        //Check if it is of type EquipmentData
        if(equipmentTool != null)
        {
            //Get the tool type
            EquipmentData.ToolType toolType = equipmentTool.toolType;

            switch (toolType)
            {
                case EquipmentData.ToolType.Shovel:
                    
                    //Remove the crop from the soil
                    if(cropPlanted != null)
                    {
                        Destroy(cropPlanted.gameObject);
                        SwitchLandStatus(LandStatus.Default);

                        break;
                    }
                    SwitchLandStatus(LandStatus.Curved);
                    break;

                case EquipmentData.ToolType.WateringCan:
                    SwitchLandStatus(LandStatus.Watered);
                    break;

                case EquipmentData.ToolType.Compost:
                    SwitchLandStatus(LandStatus.Compost);
                    break;

                case EquipmentData.ToolType.Stick:
                    SwitchLandStatus(LandStatus.Stick);
                    break;

                case EquipmentData.ToolType.Sickle:
                    SwitchLandStatus(LandStatus.Harvested);
                    break;

                case EquipmentData.ToolType.PH:
                    
                    break;
            }
            //We don't need to check for seeds if we have already confirmed the tool to be a equipment
            return;
        }
        //Try asting the itemdata in the toolslot as SeedData
        SeedData seedTool = toolSlot as SeedData;
        
        ///Conditions for the player to able to plant a seed
        ///1: He is holding a tool of type SeedData
        ///2: The Land State must be either watered or farmland
        ///3. There isn't already a crop that has been planted
        if (seedTool != null && (landStatus == LandStatus.Curved || (landStatus == LandStatus.Compost && filter.mesh.name == "Cube.002 Instance"))&& cropPlanted == null)
        {
            //Instantiate the crop object parented to the land
            GameObject cropObject = Instantiate(cropPrefab, transform);

            //Access the CropBehaviour of the crop we're going to plant
            cropPlanted = cropObject.GetComponent<CropBehaviour>();

            //Plant it with the seed's information
            cropPlanted.Plant(seedTool, this);

            //Consume the item
            InventoryManager.Instance.ConsumeItem(InventoryManager.Instance.GetEquippedSlot(InventorySlot.InventoryType.Tool));
        }
    }

    public void ClockUpdate(GameTimestamp timestamp)
    {
        timeWatered2 = timestamp;
        //Checked if 24 hours has passed since last watered
        if(landStatus == LandStatus.Watered)
        {
            //Hours since the land wass watered
            int hoursElapsed = GameTimestamp.CompareTimestamps(timeWatered, timestamp);
            //Grow the planted crop, if any

            Debug.Log(hoursElapsed);
            if(hoursElapsed >= 1)
            {
                if (cropPlanted != null)
                {
                    cropPlanted.Grow();
                }
                //Dry up (Switch back to soil
                SwitchLandStatus(LandStatus.Soil);
            }
        }
    }

    
}