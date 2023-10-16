using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDeliveryMenuSO", menuName = "Custom/DeliveryMenuSO")]
public class DeliveryMenuSO : ScriptableObject
{
    public List<DeliveryRecipeSO> DeliveryMenuSOList = new List<DeliveryRecipeSO>();
}
