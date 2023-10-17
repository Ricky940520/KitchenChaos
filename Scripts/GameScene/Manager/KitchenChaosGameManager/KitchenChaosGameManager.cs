using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenChaosGameManager : MonoBehaviour
{
    public static KitchenChaosGameManager Instance;

    public enum GameState
    {
        WaittingToStart,
        CountDown,
        PlayingGame,
        GameOver,
        Nane
    }

    private GameState state;

    private float waittingToStartTimer = 1.5f;
    private readonly float waittingToStartTimerMax = 1.5f;
    private float countDownTimer = 3f;
    private readonly float countDownTimerMax = 3f;
    private float PlayingTimer = 30f;
    private readonly float PlayingTimerMax = 30f;

    public Action<GameState> OnStateChanged;
    public Action<bool> OnGamePaused;

    private bool isGamePaused = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnGamePaused += GameInput_OnGamePaused;
    }

    public void GameInput_OnGamePaused()
    {
        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        OnGamePaused?.Invoke(isGamePaused);
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.WaittingToStart:

                waittingToStartTimer -= Time.deltaTime;

                if (waittingToStartTimer < 0)
                {
                    state = GameState.CountDown;
                    OnStateChanged?.Invoke(state);
                }

                break;

            case GameState.CountDown:
                countDownTimer -= Time.deltaTime;

                if (countDownTimer < 0)
                {
                    state = GameState.PlayingGame;
                    OnStateChanged?.Invoke(state);
                }

                break;

            case GameState.PlayingGame:
                PlayingTimer -= Time.deltaTime;

                if (PlayingTimer < 0)
                {
                    state = GameState.GameOver;
                    OnStateChanged?.Invoke(state);
                }

                break;

            case GameState.GameOver:
                OnStateChanged?.Invoke(state);
                break;
        }
    }

    public float GetCountDownTimer()
    {
        return countDownTimer;
    }

    public float GetPlayingTimerNormalized()
    {
        return 1 - (PlayingTimer / PlayingTimerMax);
    }
}
