using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlateIconSingleManager : MonoBehaviour
{
    public void SetPlateIconSingle(KitchenObjectSO kitchenObjectSO)
    {
        this.GetComponent<Image>().sprite = kitchenObjectSO.Sprite;
    }
}
