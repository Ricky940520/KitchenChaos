using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// prototype of counter
/// </summary>
public class BaseCounter : MonoBehaviour, ISetKitchenObject
{
    /// <summary>
    /// the point of kitchen object spawn
    /// </summary>
    [SerializeField] private Transform counterTopPoint;

    /// <summary>
    /// Counter's kitchen object's Data
    /// </summary>
    protected KitchenObject kitchenObject = null;

    /// <summary>
    /// Counter's kitchen object's Transform
    /// </summary>
    protected Transform kitchenObjectTransform = null;

    /// <summary>
    /// play sound
    /// </summary>
    public static Action OnKitchenObjectDropedPlaySound;

    public static void ResetAll()
    {
        OnKitchenObjectDropedPlaySound = null;
    }

    /// <summary>
    /// when ClearCounter has kitchen object return true otherwise false
    /// </summary>
    /// <returns></returns>
    public bool CounterHasKitchenObject()
    {
        return kitchenObject != null;
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
        //Debug.LogError("should not be here!!!");
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

        OnKitchenObjectDropedPlaySound?.Invoke();
    }

    public void SetKitchenObjectToNull()
    {
        this.kitchenObject = null;
        kitchenObjectTransform = null;
    }
}
