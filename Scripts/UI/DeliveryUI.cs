using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject template;


    private void DeliveryManager_OnRecipeSpawn()
    {
        UpdateUI();
    }

    private void DeliveryManager_OnDeliverCompleted()
    {
        UpdateUI();
    }

    private void Start()
    {
        template.SetActive(false);
        DeliveryManager.Instance.OnRecipeSpawn += DeliveryManager_OnRecipeSpawn;
        DeliveryManager.Instance.OnDeliveryCompleted += DeliveryManager_OnDeliverCompleted;
    }

    private void UpdateUI()
    {
        foreach (Transform child in container)
        {
            if (child == template.transform)
            {
                continue;
            }
            else
            {
                Destroy(child.gameObject);
            }

        }

        foreach (DeliveryRecipeSO deliveryRecipeSO in DeliveryManager.Instance.GetDeliveryWishesList())
        {
            GameObject go = Instantiate(template, container);
            go.SetActive(true);

            go.GetComponent<DeliveryUISingle>().SetRecipeName(deliveryRecipeSO);
            go.GetComponent<DeliveryUISingle>().SetIcon(deliveryRecipeSO.DeliveryRecipeSOList);
        }
    }
}
