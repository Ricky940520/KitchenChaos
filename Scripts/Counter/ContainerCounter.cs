using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    /// <summary>
    /// Fire container counter's animator
    /// </summary>
    public Action OnContainerCounterInteractFireAnimator;

    /// <summary>
    /// when E pressed 
    /// </summary>
    public override void Interact(PlayerInteract player)
    {

        if (!player.PlayerHasKitchenObject())
        {
            //when player has nothing 
            //player should grab kitchen object
            SpawnKitchenObject();
            player.SetKitchenObject(kitchenObject);

            OnContainerCounterInteractFireAnimator?.Invoke();
        }


    }
}
