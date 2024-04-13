using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatGiver : MonoBehaviour
{
    private Stat statComponent;
    int micro;
    int hara;
    // Start is called before the first frame update
    void Start()
    {
        // Assign the Stat component
        statComponent = GetComponent<Stat>();
        micro = statComponent.Microorganism;
        hara = statComponent.ZatHara;
    }

    public void changeMicro(int change, Boolean add)
    {
        if(add == true)
        {
            micro += change;
        }
        else
        {
            micro -= change;
        }
    }

    public void changeHara(int change, Boolean add)
    {
        if(add == true)
        {
            hara += change;
        }
        else
        {
            hara -= change;
        }
    }

    public int giveMicroo()
    {
        // Check if statComponent is null before accessing its properties
        if (statComponent != null)
        {
            return micro;
        }
        else
        {
            Debug.LogError("Stat component not found on the same GameObject as StatGiver.");
            return 0; // Return a default value or handle the error appropriately
        }
    }

    public int giveHara()
    {
        // Check if statComponent is null before accessing its properties
        if (statComponent != null)
        {
            return hara;
        }
        else
        {
            Debug.LogError("Stat component not found on the same GameObject as StatGiver.");
            return 0; // Return a default value or handle the error appropriately
        }
    }
}
