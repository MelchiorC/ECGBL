using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NoMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Boolean OnUI = false;
    public Collider isIt;
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Tes");
        }
    }

   

    private void OnMouseExit()
    {
        // Reset the flag indicating that the mouse is over the UI
        OnUI = false;
    }
}
