using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryUISingle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeName;
    [SerializeField] private Transform IconContainer;
    [SerializeField] private Transform template;

    private void Start()
    {
        template.gameObject.SetActive(false);
    }

    public void SetRecipeName(DeliveryRecipeSO deliveryRecipeSO)
    {
        recipeName.text = deliveryRecipeSO.RecipeSOName;
    }

    public void SetIcon(List<KitchenObjectSO> kitchenObjectSOList)
    {
        foreach (KitchenObjectSO kitchenObjectSO in kitchenObjectSOList)
        {
            GameObject go = Instantiate(template.gameObject, IconContainer);
            go.GetComponent<Image>().sprite = kitchenObjectSO.Sprite;
            go.SetActive(true);
        }
    }
}
