using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;

    /// <summary>
    /// map corresponding kitchenObject SO and GameObject
    /// </summary>
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO KitchenObjectSO;
        public GameObject GameObject;
    }

    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSO_GameObjectList = new List<KitchenObjectSO_GameObject>();

    private void Start()
    {
        plateKitchenObject.OnKitchenObjectIngredientListChanged += PlateKitchenObject_OnKitchenObjectIngredientListChanged;

        foreach (var item in kitchenObjectSO_GameObjectList)
        {
            item.GameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Update the plate complete gameobject visual
    /// </summary>
    private void PlateKitchenObject_OnKitchenObjectIngredientListChanged(KitchenObjectSO kitchenObjectSO)
    {
        foreach (var item in kitchenObjectSO_GameObjectList)
        {
            if (item.KitchenObjectSO == kitchenObjectSO)
            {
                item.GameObject.SetActive(true);
            }
        }
    }

}
