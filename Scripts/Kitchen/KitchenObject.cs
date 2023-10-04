using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Every kitchen object has this data
/// </summary>
public class KitchenObject : MonoBehaviour
{
    /// <summary>
    /// Kitchen Object Scriptable Object 
    /// </summary>
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    /// <summary>
    /// Get Kitchen Object's Data
    /// </summary>
    /// <returns></returns>
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}
