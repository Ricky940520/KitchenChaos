using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    /// <summary>
    /// only cooked valid kitchen object 
    /// </summary>
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectList = new List<KitchenObjectSO>();

    /// <summary>
    /// Dish ingredient list
    /// </summary>
    private List<KitchenObjectSO> DishIngredientList = new List<KitchenObjectSO>();

    /// <summary>
    /// Update the plate complete visual
    /// </summary>
    public Action<KitchenObjectSO> OnKitchenObjectIngredientListChanged;


    /// <summary>
    /// Try Add Ingredient
    /// </summary>
    /// <param name="kitchenObject"></param>
    /// <returns></returns>
    public bool TryAddIngredient(KitchenObject kitchenObject)
    {
        if (!validKitchenObjectList.Contains(kitchenObject.GetKitchenObjectSO()))
        {
            //when kitchen object is not valid
            return false;
        }
        else
        {
            if (DishIngredientList.Contains(kitchenObject.GetKitchenObjectSO()))
            {
                //when the ingredient is duplicate
                return false;
            }
            else
            {
                DishIngredientList.Add(kitchenObject.GetKitchenObjectSO());
                OnKitchenObjectIngredientListChanged?.Invoke(kitchenObject.GetKitchenObjectSO());
                return true;
            }
        }
    }

    public List<KitchenObjectSO> GetDishIngredientList()
    {
        return DishIngredientList;
    }

}
