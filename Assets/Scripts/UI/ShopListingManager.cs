using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopListingManager : MonoBehaviour
{
    //The shop Listing entry prefab to instantiate
    public GameObject shopListing;
    //The transform of the grid to instantiate the entries on
    public Transform listingGrid;

    public void RenderShop(List<ItemData> shopItems)
    {
        //Reset the listings if there was a previous one
        if(listingGrid.childCount > 0)
        {
            foreach(Transform child in listingGrid)
            {
                Destroy(child.gameObject);
            }
        }

        //Create a new listing for every item
        foreach(ItemData shopItem in shopItems)
        {
            //Instantiate a shop listing prefab for the item
            GameObject listingGameObject = Instantiate(shopListing, listingGrid);

            //Assign it the shop item and display listing
            listingGameObject.GetComponent<ShopListing>().Display(shopItem);
        }
    }
}
