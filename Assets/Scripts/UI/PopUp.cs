using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject Hover;

    void Start()
    {
        Hover = GameObject.Find("Scripts/UI/PopUp");
        if (Hover != null)
        {
            Debug.Log("GameObject found");
            Hover.SetActive(false);
        }
        else
        {
            Debug.LogWarning("GameObject '3dPopUp' not found!");
        }
        Hover.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Hover.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Hover.SetActive(false);
    }
    public void ReceiveHighlightedPosition(Vector3 position)
    {
        // Do something with the received position data
        Debug.Log("Received Highlighted Position: " + position);
    }
}

