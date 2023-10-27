using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    private int plateSpawnAmount = 0;
    private int plateSpawnAmountMax = 4;

    private float plateSpawnTimer = 0;
    private float plateSpawnTimerMax = 4f;

    /// <summary>
    /// Kitchen Object Scriptable Object (be prefab)
    /// </summary>
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    /// <summary>
    /// all plates in this list
    /// </summary>
    private List<Transform> plateTransformList = new List<Transform>();

    private void Update()
    {
        if (KitchenChaosGameManager.Instance.GetGameState() != KitchenChaosGameManager.GameState.PlayingGame)
        {
            return;
        }

        if (plateSpawnAmount < plateSpawnAmountMax)
        {
            //when the plate can be spawned
            plateSpawnTimer += Time.deltaTime;

            if (plateSpawnTimer >= plateSpawnTimerMax)
            {
                //spawn the plate
                SpawnPlate();
                plateSpawnTimer = 0;
            }
        }
    }

    /// <summary>
    /// spawn plate
    /// </summary>
    private void SpawnPlate()
    {
        if (kitchenObjectSO != null)
        {
            Transform plateTransform = Instantiate(kitchenObjectSO.Prefab, GetCounterTopPoint());
            float offsetY = 0.1f;
            plateTransform.SetLocalPositionAndRotation(new Vector3(0, offsetY * plateTransformList.Count, 0), Quaternion.identity);
            plateTransformList.Add(plateTransform);
            plateSpawnAmount++;

            kitchenObject = plateTransform.GetComponent<KitchenObject>();
            kitchenObjectTransform = plateTransform;
        }
    }

    /// <summary>
    /// Remove plate
    /// </summary>
    private void RemovePlate()
    {
        if (plateSpawnAmount > 0)
        {
            plateTransformList.RemoveAt(plateTransformList.Count - 1);
            plateSpawnAmount--;

            if (plateSpawnAmount == 0)
            {
                SetKitchenObjectToNull();
            }
            else
            {
                kitchenObject = plateTransformList[plateTransformList.Count - 1].GetComponent<KitchenObject>();
                kitchenObjectTransform = plateTransformList[plateTransformList.Count - 1];
            }
        }
    }

    public override void Interact(PlayerInteract player)
    {
        if (CounterHasKitchenObject())
        {
            //when counter has plate on it
            if (!player.PlayerHasKitchenObject())
            {
                //when player has nothing on it
                player.SetKitchenObject(kitchenObject);
                RemovePlate();
            }
        }
    }
}
