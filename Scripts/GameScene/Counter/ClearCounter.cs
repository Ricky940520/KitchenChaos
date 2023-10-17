using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    /// <summary>
    /// when E pressed 
    /// </summary>
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
            }
        }
    }
}
