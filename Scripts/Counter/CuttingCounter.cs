using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    /// <summary>
    /// Mapping kitchen object to kitchen object slices
    /// Key is kitchen object and Value is kitchen object slices
    /// </summary>
    private Dictionary<KitchenObjectSO, KitchenObjectSO> kitchenObjectSlicedSODic;

    /// <summary>
    /// All cutting recipe array
    /// </summary>
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    /// <summary>
    /// Kitchen object has been cut times
    /// </summary>
    private int cutProcessCount;

    /// <summary>
    /// update UI when cut process count changed
    /// </summary>
    public Action<float> OnCutProcessCountChangedUpdateUI;

    /// <summary>
    /// fire animation when cut process count changed
    /// </summary>
    public Action OnCutProcessCountChangedFireAnimator;

    private void Awake()
    {
        kitchenObjectSlicedSODic = new Dictionary<KitchenObjectSO, KitchenObjectSO>();

    }

    private void Start()
    {
        foreach (var element in cuttingRecipeSOArray)
        {
            kitchenObjectSlicedSODic.Add(element.KitchenObjectSO, element.KitchenObjectSlicesSO);
        }
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
                cutProcessCount = 0;
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
            if (kitchenObjectSlicedSODic.ContainsKey(kitchenObject.GetKitchenObjectSO()))
            {
                //when the kitchen object can be cut(only 3 things can be cut)
                CutKitchenObject();
            }

        }
    }

    /// <summary>
    /// Cut KitchenObjectSO
    /// </summary>
    private void CutKitchenObject()
    {
        cutProcessCount++;
        int cutProcessMaximum = 0;

        foreach (var element in cuttingRecipeSOArray)
        {
            if (element.KitchenObjectSO == kitchenObject.GetKitchenObjectSO())
            {
                cutProcessMaximum = element.CutProcessMaximum;
            }
        }

        float cutProcessPercent = (float)cutProcessCount / cutProcessMaximum;
        OnCutProcessCountChangedUpdateUI?.Invoke(cutProcessPercent);
        OnCutProcessCountChangedFireAnimator?.Invoke();

        if (cutProcessCount >= cutProcessMaximum)
        {
            kitchenObjectSlicedSODic.TryGetValue(kitchenObject.GetKitchenObjectSO(), out KitchenObjectSO prefab);

            kitchenObjectTransform = Instantiate(prefab.Prefab, GetCounterTopPoint());
            kitchenObjectTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            Destroy(kitchenObject.gameObject);
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

            cutProcessCount = 0;
        }
    }
}
