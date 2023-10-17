using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreImage;

    private void Start()
    {
        KitchenChaosGameManager.Instance.OnStateChanged += KitchenChaosGameManager_OnStateChanged;
        Hide();
    }

    private void KitchenChaosGameManager_OnStateChanged(KitchenChaosGameManager.GameState state)
    {
        if (state == KitchenChaosGameManager.GameState.GameOver)
        {
            scoreImage.text = DeliveryManager.Instance.GetDeliveryScore().ToString();
            Show();
        }
        else
        {
            Hide();

        }
    }

    private void Show()
    {
        this.gameObject.SetActive(true);
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
