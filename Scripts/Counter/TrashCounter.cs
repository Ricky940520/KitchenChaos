using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(PlayerInteract player)
    {
        if (player.PlayerHasKitchenObject())
        {
            player.GetPlayerKitchenObject().DestroySelf();
            player.SetKitchenObjectToNull();
        }
    }
}
