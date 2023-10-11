using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    /// <summary>
    /// only cooked kitchen object is valid
    /// </summary>
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectList = new List<KitchenObjectSO>();

    /// <summary>
    /// All ingredient list
    /// </summary>
    private List<KitchenObjectSO> kitchenObjectIngredientList = new List<KitchenObjectSO>();


    /// <summary>
    /// Try Add Ingredient
    /// </summary>
    /// <param name="kitchenObject"></param>
    /// <returns></returns>
    public bool TryAddIngredient(KitchenObject kitchenObject)
    {
        if (!validKitchenObjectList.Contains(kitchenObject.GetKitchenObjectSO()))
        {
            return false;
        }
        else
        {
            if (kitchenObjectIngredientList.Contains(kitchenObject.GetKitchenObjectSO()))
            {
                return false;
            }
            else
            {
                kitchenObjectIngredientList.Add(kitchenObject.GetKitchenObjectSO());
                return true;
            }
        }
    }

}
