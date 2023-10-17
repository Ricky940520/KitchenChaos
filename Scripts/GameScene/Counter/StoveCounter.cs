using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgressBar
{
    /// <summary>
    /// All frying recipe array
    /// </summary>
    [SerializeField] private FryingRecipeSO[] fryRecipeSOArray;

    /// <summary>
    /// if the meat is frying
    /// </summary>
    private bool isFrying;

    /// <summary>
    /// Frying timer
    /// </summary>
    private float FryingMeatTimer;

    /// <summary>
    /// your current frying recipe 
    /// </summary>
    private FryingRecipeSO currentFryingRecipeSO;

    /// <summary>
    /// active the stove visual(particle) when is frying
    /// </summary>
    public Action<bool> OnIsFryingChanged;

    /// <summary>
    /// Update Progress Bar
    /// </summary>
    public Action<float> OnProgressBarChanged { get; set; }

    private void Start()
    {
        isFrying = false;
        FryingMeatTimer = 0;
        currentFryingRecipeSO = null;
    }

    private void Update()
    {
        StartFryingMeat();
    }

    public override void Interact(PlayerInteract player)
    {
        if (!CounterHasKitchenObject())
        {
            //when counter has nothing on it
            if (player.PlayerHasKitchenObject())
            {
                //player is grabing something
                foreach (FryingRecipeSO item in fryRecipeSOArray)
                {
                    if (player.GetPlayerKitchenObject().GetKitchenObjectSO() == item.InputMeatPattyKitchenObjectSO)
                    {
                        currentFryingRecipeSO = item;
                        isFrying = true;
                        this.SetKitchenObject(player.GetPlayerKitchenObject());
                        OnIsFryingChanged?.Invoke(isFrying);
                        break;
                    }
                }
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
                        Refrying();
                        OnIsFryingChanged?.Invoke(isFrying);
                        OnProgressBarChanged?.Invoke(1);
                        kitchenObject.DestroySelf();
                    }
                }
                else if (kitchenObject.TryGetPlate(out plateKitchenObject))
                {
                    //if Counter has plate on it                   
                    if (plateKitchenObject.TryAddIngredient(player.GetPlayerKitchenObject()))
                    {
                        Refrying();
                        OnIsFryingChanged?.Invoke(isFrying);
                        OnProgressBarChanged?.Invoke(1);
                        player.GetPlayerKitchenObject().DestroySelf();
                    }
                }
            }
            else
            {
                //player is not grabing anything
                //if you take away the meat that you are frying 
                Refrying();
                OnIsFryingChanged?.Invoke(isFrying);
                OnProgressBarChanged?.Invoke(1);
                player.SetKitchenObject(kitchenObject);
            }
        }
    }

    /// <summary>
    /// Frying Meat
    /// </summary>
    private void StartFryingMeat()
    {
        if (isFrying)
        {
            FryingMeatTimer += Time.deltaTime;
            float fryProgressPercent = FryingMeatTimer / currentFryingRecipeSO.CookingTime;

            OnProgressBarChanged?.Invoke(fryProgressPercent);

            if (FryingMeatTimer >= currentFryingRecipeSO.CookingTime)
            {
                Destroy(kitchenObjectTransform.gameObject);
                kitchenObjectTransform = Instantiate(currentFryingRecipeSO.OutputMeatPattyKitchenObjectSO.Prefab, GetCounterTopPoint());
                kitchenObjectTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

                kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

                int index = Array.IndexOf(fryRecipeSOArray, currentFryingRecipeSO);

                switch (index)
                {
                    case 0:
                        isFrying = true;
                        FryingMeatTimer = 0;
                        currentFryingRecipeSO = fryRecipeSOArray[1];
                        break;
                    case 1:
                        isFrying = false;
                        FryingMeatTimer = 0;
                        currentFryingRecipeSO = null;
                        OnIsFryingChanged?.Invoke(isFrying);
                        OnProgressBarChanged?.Invoke(1);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// if you take away the meat that you are frying 
    /// you need to refrying
    /// </summary>
    private void Refrying()
    {
        isFrying = false;
        FryingMeatTimer = 0;
        currentFryingRecipeSO = null;
    }
}
