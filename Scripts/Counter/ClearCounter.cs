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
                //do nothing
            }
            else
            {
                //player is not grabing anything
                player.SetKitchenObject(kitchenObject);
            }
        }
    }
}
