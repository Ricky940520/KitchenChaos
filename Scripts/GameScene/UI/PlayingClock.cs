using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingClock : MonoBehaviour
{
    [SerializeField] private Image clock;

    private bool isPlaying = false;

    private void Start()
    {
        KitchenChaosGameManager.Instance.OnStateChanged += KitchenChaosGameManager_OnStateChanged;
        Hide();
    }

    private void KitchenChaosGameManager_OnStateChanged(KitchenChaosGameManager.GameState state)
    {
        if (state == KitchenChaosGameManager.GameState.PlayingGame)
        {
            isPlaying = true;
            Show();
        }
        else
        {
            isPlaying = false;
            Hide();

        }
    }

    private void Update()
    {
        if (isPlaying)
        {
            clock.fillAmount = KitchenChaosGameManager.Instance.GetPlayingTimerNormalized();
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
