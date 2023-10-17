using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    /// <summary>
    /// Kitchen Object Scriptable Object (be prefab)
    /// </summary>
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    /// <summary>
    /// Fire container counter's animator
    /// </summary>
    public Action OnContainerCounterInteractFireAnimator;

    /// <summary>
    /// Spawn Kitchen Object
    /// </summary>
    private void SpawnKitchenObject()
    {
        if (kitchenObjectSO != null)
        {
            kitchenObjectTransform = Instantiate(kitchenObjectSO.Prefab, GetCounterTopPoint());
            kitchenObjectTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();
        }
    }

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
