using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Create new crafting recipe")]
public class RecipeData : ScriptableObject
{
    public List<ItemData> Ingredients;
    public ItemData craftResult;
}
