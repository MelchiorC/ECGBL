using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnItemClick : MonoBehaviour, IPointerClickHandler
{
    public CompostShower compost;
    // Start is called before the first frame update
    public void OnPointerClick(PointerEventData eventData)
    {
        compost.gameObject.GetComponent<CompostShower>().itemMovement();
    }
}
