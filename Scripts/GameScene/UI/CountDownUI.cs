using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDown;

    private bool isCountingDown = false;

    private void Start()
    {
        KitchenChaosGameManager.Instance.OnStateChanged += KitchenChaosGameManager_OnStateChanged;
        Hide();
    }

    private void KitchenChaosGameManager_OnStateChanged(KitchenChaosGameManager.GameState state)
    {
        if (state == KitchenChaosGameManager.GameState.CountDown)
        {
            Show();
            isCountingDown = true;
        }
        else
        {
            Hide();
            isCountingDown = false;
        }
    }

    private void Update()
    {
        if (isCountingDown)
        {
            countDown.text = math.ceil(KitchenChaosGameManager.Instance.GetCountDownTimer()).ToString();
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
