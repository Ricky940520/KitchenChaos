using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public override void Interact(PlayerInteract player)
    {
        if (player.PlayerHasKitchenObject())
        {
            //when player is grabing something
            if (player.GetPlayerKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //when player is grabing a plate(or plate kitchen object)
                DeliveryManager.Instance.Delivery(plateKitchenObject);

                player.GetPlayerKitchenObject().DestroySelf();
            }
        }
    }
}
