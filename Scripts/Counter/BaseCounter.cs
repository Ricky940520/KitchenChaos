using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// prototype of counter
/// </summary>
public class BaseCounter : MonoBehaviour, IHoldKitchenObject
{
    /// <summary>
    /// Kitchen Object Scriptable Object (be prefab)
    /// </summary>
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    /// <summary>
    /// the point of kitchen object spawn
    /// </summary>
    [SerializeField] private Transform counterTopPoint;

    /// <summary>
    /// kitchen object's Data
    /// </summary>
    protected KitchenObject kitchenObject = null;

    /// <summary>
    /// kitchen object's Transform
    /// </summary>
    protected Transform kitchenObjectTransform = null;

    /// <summary>
    /// when ClearCounter has kitchen object return true otherwise false
    /// </summary>
    /// <returns></returns>
    public bool CounterHasKitchenObject()
    {
        return kitchenObject != null;
    }

    /// <summary>
    /// Spawn Kitchen Object
    /// </summary>
    public virtual void SpawnKitchenObject()
    {
        if (kitchenObjectSO != null)
        {
            kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab, GetCounterTopPoint());
            kitchenObjectTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        }

    }

    /// <summary>
    /// when E pressed 
    /// </summary>
    public virtual void Interact(PlayerInteract player)
    {
        Debug.LogError("should not be here!!!");
    }

    /// <summary>
    /// when F pressed
    /// </summary>
    /// <param name="player"></param>
    public virtual void InteractAlternate(PlayerInteract player)
    {
        Debug.LogError("should not be here!!!");
    }

    /// <summary>
    /// return the point of kitchen object spawn
    /// </summary>
    /// <returns></returns>
    public Transform GetCounterTopPoint()
    {
        return counterTopPoint;
    }

    /// <summary>
    /// Drop kitchen object to BaseCounter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target">where you want to drop</param>
    /// <param name="kitchenObject"></param>
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        this.kitchenObject.transform.SetParent(GetCounterTopPoint(), false);
        this.kitchenObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        kitchenObjectTransform = this.kitchenObject.transform;

        PlayerInteract.Instance.SetKitchenObjectToNull();
    }

    public void SetKitchenObjectToNull()
    {
        this.kitchenObject = null;
        kitchenObjectTransform = null;
    }

    public void DestroyKitchenObject()
    {
        Destroy(kitchenObject.gameObject);
        SetKitchenObjectToNull();
    }

}
