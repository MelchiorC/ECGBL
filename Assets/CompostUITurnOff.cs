using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompostUITurnOff : MonoBehaviour
{
    public GameObject CompostUI;
    // Start is called before the first frame update
    public int show()
    {
        if (CompostUI.active == true)
        {

            return 10;
        }
        else
        {
            return 11;
        }
    }
}
