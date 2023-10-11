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

    /// <summary>
    /// Set kitchen object data
    /// </summary>
    /// <param name="obj"></param>
    public void SetKitchenObjectSO(KitchenObjectSO obj)
    {
        this.kitchenObjectSO = obj;
    }

    /// <summary>
    /// Destroy
    /// </summary>
    public void DestroySelf()
    {
        Destroy(this.gameObject);
        this.kitchenObjectSO = null;
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
}
