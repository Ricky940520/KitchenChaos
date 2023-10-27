using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    /// <summary>
    /// delivery wishes list
    /// </summary>
    private List<DeliveryRecipeSO> deliveryWishesList = new List<DeliveryRecipeSO>();
    private float timerOfSpawnDeliveryWishes = 0;
    private readonly float timerOfSpawnDeliveryWishesMax = 3;
    private readonly int deliveryWishesMax = 4;

    [SerializeField] private DeliveryMenuSO deliveryMenu;

    //about logic event
    public Action OnRecipeSpawn;
    public Action OnDeliveryCompleted;
    public Action OnDeliveryFailed;

    //about sound event
    public Action OnDeliveryFailedPlaySound;
    public Action OnDeliveryCompletedPlaySound;

    private bool isPlayingGame = false;

    private int deliveryScore = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        KitchenChaosGameManager.Instance.OnStateChanged += KitchenChaosGameManager_OnStateChanged;
    }

    private void KitchenChaosGameManager_OnStateChanged(KitchenChaosGameManager.GameState gameState)
    {
        if (gameState == KitchenChaosGameManager.GameState.PlayingGame)
        {
            isPlayingGame = true;
        }
        else
        {
            isPlayingGame = false;
        }
    }

    private void Update()
    {
        RecipeSpawn();
    }

    private void RecipeSpawn()
    {
        if (isPlayingGame)
        {
            if (deliveryWishesList.Count < deliveryWishesMax)
            {
                timerOfSpawnDeliveryWishes += Time.deltaTime;

                if (timerOfSpawnDeliveryWishes >= timerOfSpawnDeliveryWishesMax)
                {
                    deliveryWishesList.Add(deliveryMenu.DeliveryMenuSOList[UnityEngine.Random.Range(0, deliveryMenu.DeliveryMenuSOList.Count)]);
                    timerOfSpawnDeliveryWishes = 0;
                    OnRecipeSpawn?.Invoke();
                }
            }
        }
    }

    /// <summary>
    /// return ture when delivery is valid otherwise false 
    /// </summary>
    /// <param name="plateKitchenObject"></param>
    /// <returns></returns>
    public void Delivery(PlateKitchenObject plateKitchenObject)
    {
        bool isFound = false;

        for (int i = 0; i < deliveryWishesList.Count; i++)
        {
            //linq
            if (deliveryWishesList[i].DeliveryRecipeSOList.All(plateKitchenObject.GetDishIngredientList().Contains))
            {
                deliveryWishesList.Remove(deliveryWishesList[i]);
                isFound = true;
                deliveryScore++;

                OnDeliveryCompleted?.Invoke();
                OnDeliveryCompletedPlaySound?.Invoke();
                break;
            }
        }

        if (!isFound)
        {
            OnDeliveryFailed?.Invoke();
            OnDeliveryFailedPlaySound?.Invoke();
        }
    }

    public List<DeliveryRecipeSO> GetDeliveryWishesList()
    {
        return deliveryWishesList;
    }

    public int GetDeliveryScore()
    {
        return deliveryScore;
    }
}

