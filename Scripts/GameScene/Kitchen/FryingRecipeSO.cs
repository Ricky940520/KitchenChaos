using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFryRecipeSO", menuName = "Custom/FryRecipeSO")]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO InputMeatPattyKitchenObjectSO;
    public KitchenObjectSO OutputMeatPattyKitchenObjectSO;

    public float CookingTime;

    public KitchenObjectSO GetOutputMeatPattyFromInput(KitchenObjectSO input)
    {
        if (input != null)
        {
            if (input == this.InputMeatPattyKitchenObjectSO)
            {
                return OutputMeatPattyKitchenObjectSO;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}
