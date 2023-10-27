using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class CountDownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countDown;

    private Animator animator;

    private int previousCountDownNumber = 0;
    private int currentCountDwonNumber = 0;

    private const string ANIMATOR_TRIGGER = "CounttingDown";

    private bool isCountingDown = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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
            currentCountDwonNumber = Mathf.CeilToInt(KitchenChaosGameManager.Instance.GetCountDownTimer());
            countDown.text = currentCountDwonNumber.ToString();

            if (previousCountDownNumber != currentCountDwonNumber)
            {
                previousCountDownNumber = currentCountDwonNumber;
                animator.SetTrigger(ANIMATOR_TRIGGER);
                SoundManager.instance.PlayWarningSound();
            }
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
