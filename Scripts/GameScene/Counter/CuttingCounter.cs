using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgressBar
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
    /// fire animation when cut process count changed
    /// </summary>
    public Action OnCutProcessCountChangedFireAnimator;

    /// <summary>
    /// Play Sound Effect
    /// </summary>
    public static Action<CuttingCounter> OnCuttingPlaySound;

    /// <summary>
    /// Update Progress Bar
    /// </summary>
    public Action<float> OnProgressBarChanged { get; set; }

    new public static void ResetAll()
    {
        OnCuttingPlaySound = null;
    }

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

        cutProcessCount = 0;
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
                if (player.GetPlayerKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //player is grabing plate
                    if (plateKitchenObject.TryAddIngredient(kitchenObject))
                    {
                        kitchenObject.DestroySelf();
                    }
                }
                else if (kitchenObject.TryGetPlate(out plateKitchenObject))
                {
                    //if Counter has plate on it                   
                    if (plateKitchenObject.TryAddIngredient(player.GetPlayerKitchenObject()))
                    {
                        player.GetPlayerKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                //player is not grabing anything
                player.SetKitchenObject(kitchenObject);
                OnProgressBarChanged?.Invoke(1);
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
        OnProgressBarChanged?.Invoke(cutProcessPercent);
        OnCutProcessCountChangedFireAnimator?.Invoke();
        OnCuttingPlaySound?.Invoke(this);

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
