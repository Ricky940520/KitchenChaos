using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCuttingRecipeSO", menuName = "Custom/CuttingRecipeSO")]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO KitchenObjectSO;
    public KitchenObjectSO KitchenObjectSlicesSO;
    public int CutProcessMaximum;
}
