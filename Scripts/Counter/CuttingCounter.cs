using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    /// <summary>
    /// Mapping kitchen object to kitchen object slices
    /// </summary>
    private Dictionary<KitchenObjectSO, KitchenObjectSO> kitchenObjectSlicedSODic;

    [SerializeField] private KitchenObjectSO[] kitchenObjectArray;

    private void Awake()
    {
        kitchenObjectSlicedSODic = new Dictionary<KitchenObjectSO, KitchenObjectSO>();
    }

    private void Start()
    {
        kitchenObjectSlicedSODic.Add(kitchenObjectArray[0], kitchenObjectArray[3]);
        kitchenObjectSlicedSODic.Add(kitchenObjectArray[1], kitchenObjectArray[4]);
        kitchenObjectSlicedSODic.Add(kitchenObjectArray[2], kitchenObjectArray[5]);
    }

    public override void Interact(PlayerInteract player)
    {
        if (!CounterHasKitchenObject())
        {
            //when counter has nothing on it
            if (player.PlayerHasKitchenObject())
            {
                //player is grabing something
                this.SetKitchenObject(player.GetPlayerKitchenObject());
            }
        }
        else
        {
            //when counter has something on it
            if (player.PlayerHasKitchenObject())
            {
                //player is grabing something
                //do nothing
            }
            else
            {
                //player is not grabing anything
                player.SetKitchenObject(kitchenObject);
            }
        }
    }

    public override void InteractAlternate(PlayerInteract player)
    {
        if (CounterHasKitchenObject())
        {
            //when counter has something on it
            CutKitchenObject();
        }
    }

    /// <summary>
    /// Cut KitchenObject
    /// </summary>
    private void CutKitchenObject()
    {
        if (kitchenObjectSlicedSODic.ContainsKey(kitchenObject.GetKitchenObjectSO()))
        {
            //when the kitchen object can be cut(only 3 things can be cut)
            SpawnKitchenObject();

        }
    }

    /// <summary>
    /// Spawn kitchen object slices
    /// </summary>
    public override void SpawnKitchenObject()
    {
        kitchenObjectSlicedSODic.TryGetValue(kitchenObject.GetKitchenObjectSO(), out KitchenObjectSO prefab);

        kitchenObjectTransform = Instantiate(prefab.Prefab, GetCounterTopPoint());
        kitchenObjectTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        Destroy(kitchenObject.gameObject);
        kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
    }


}
