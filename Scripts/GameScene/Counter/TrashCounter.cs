using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    /// <summary>
    /// play sound
    /// </summary>
    public static Action OnKitchenObjectTrashedPlaySound;

    new public static void ResetAll()
    {
        OnKitchenObjectTrashedPlaySound=null;
    }

    public override void Interact(PlayerInteract player)
    {
        if (player.PlayerHasKitchenObject())
        {
            player.GetPlayerKitchenObject().DestroySelf();
            player.SetKitchenObjectToNull();
            OnKitchenObjectTrashedPlaySound?.Invoke();
        }
    }
}
