using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DraggableInventoryCompost : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public CompostShower compost;
    /* public int idOrigin;// -1 if it is not bag inventor, >=0 otherwise
     public int idDest;// -1 if it is not the crafting ingredient slot, >= 0 otherwise
     public bool isResult;//true if it is the result slot, false otherwise
     public int trueDest = -1;
     public bool occupied; 
     */ //Obsolete


    
    public Vector3 originSnappingPosition;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
       // throw new System.NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        transform.Translate(eventData.delta);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //compost.gameObject.GetComponent<CompostShower>().itemMovement(); // handler for the UI to put item and process it
        
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        if (results[1].gameObject.tag == "CraftingSlotCompost" && CompostShower.instance.isCompostRecipe(gameObject.GetComponent<InventorySlot>().GetItemSlotData()))
        {
            this.gameObject.transform.position = results[1].gameObject.transform.position;
            CompostShower.instance.AddItemToRecipe(gameObject.GetComponent<InventorySlot>().GetItemSlotData());
        } else
        {
            this.gameObject.transform.position = originSnappingPosition;
            if(results[1].gameObject.tag != "CraftingSlotCompost")
            {
                CompostShower.instance.RemoveItemToRecipe(gameObject.GetComponent<InventorySlot>().GetItemSlotData());
            }
        }
    }

    // Start is called before the first frame update
    public void OnPointerClick(PointerEventData eventData)
    {
        /*int opId = -1;
        if (isResult)
        {
            opId = 2; 
        }else if(idOrigin <= -1)
        {
            opId = 1;
        }else if(idDest <= -1)
        {
            opId = 0;
        }
        compost.gameObject.GetComponent<CompostShower>().itemMovement(isResult,idDest,idOrigin,opId,trueDest);
        */
    }

}
