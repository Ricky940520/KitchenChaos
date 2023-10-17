using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconManager : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private GameObject plateIconTemplate;

    private void Start()
    {
        plateKitchenObject.OnKitchenObjectIngredientListChanged += PlateKitchenObject_OnKitchenObjectIngredientListChanged;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// update the plate icon visual
    /// </summary>
    /// <param name="kitchenObjectSO"></param>
    private void PlateKitchenObject_OnKitchenObjectIngredientListChanged(KitchenObjectSO kitchenObjectSO)
    {
        GameObject newPlateIcon = Instantiate(plateIconTemplate, transform);
        newPlateIcon.GetComponent<RectTransform>().SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        newPlateIcon.GetComponentInChildren<PlateIconSingleManager>().SetPlateIconSingle(kitchenObjectSO);
        newPlateIcon.gameObject.SetActive(true);
    }
}
