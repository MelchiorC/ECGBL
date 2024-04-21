using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TextUI : MonoBehaviour
{

    public TextMeshProUGUI txtComp;

    // Start is called before the first frame update
    void Start()
    {

        
    }

    public void shower(StatGiver stats)
    {
        // Call the give() function of StatGiver to get the Microorganism and ZatHara values
        int zatHara = stats.giveHara();
        int microorganism = stats.giveMicroo();
        
        


        // Set the text of txtComp with the obtained values
        txtComp.text = string.Format("Microorganism: {0}\n\nUnsur Hara: {1}", microorganism, zatHara);
    }
}
