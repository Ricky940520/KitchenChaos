using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDeliveryRecipeSO", menuName = "Custom/DeliveryRecipeSO")]
public class DeliveryRecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> DeliveryRecipeSOList = new List<KitchenObjectSO>();
    public string RecipeSOName;
}
