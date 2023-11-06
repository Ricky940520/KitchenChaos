using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenChaosGameManager : MonoBehaviour
{
    public static KitchenChaosGameManager Instance;

    public enum GameState
    {
        WaittingToPressInteract,
        WaittingToStart,
        CountDown,
        PlayingGame,
        GameOver,
        Nane
    }

    private GameState state = GameState.WaittingToPressInteract;

    private float waittingToStartTimer = 1.5f;
    private readonly float waittingToStartTimerMax = 1.5f;
    private float countDownTimer = 3f;
    private readonly float countDownTimerMax = 3f;
    [SerializeField] private float PlayingTimer = 30f;
    [SerializeField] private float PlayingTimerMax = 30f;

    public Action<GameState> OnStateChanged;
    public Action<bool> OnGamePaused;
    public Action OnGameResume;

    private bool isGamePaused = false;

    [SerializeField] private Transform counterPartentTransform;

    [SerializeField] private Transform partentUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnGamePaused += GameInput_OnGamePaused;
        TutorialUI.Instance.OnTutorialHide += GameStart;
    }

    public void GameInput_OnGamePaused()
    {
        if (state == GameState.WaittingToPressInteract)
        {
            return;
        }

        isGamePaused = !isGamePaused;

        if (isGamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            OnGameResume?.Invoke();
        }

        OnGamePaused?.Invoke(isGamePaused);
    }



    private void Update()
    {
        switch (state)
        {
            case GameState.WaittingToStart:

                waittingToStartTimer -= Time.deltaTime;

                //if (waittingToStartTimer < 0)
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

    public GameState GetGameState()
    {
        return state;
    }

    public void GameStart()
    {
        state = GameState.WaittingToStart;
    }

    private void GameReset()
    {
        waittingToStartTimer = waittingToStartTimerMax;
        countDownTimer = countDownTimerMax;
        PlayingTimer = PlayingTimerMax;

        foreach (Transform counter in counterPartentTransform)
        {
            if (counter.GetComponent<BaseCounter>() is PlateCounter)
            {
                continue;
            }
            counter.GetComponent<BaseCounter>().ResetCounter();
        }

        if (PlayerInteract.Instance.GetPlayerKitchenObject() != null)
        {
            PlayerInteract.Instance.GetPlayerKitchenObject().DestroySelf();
        }

        DeliveryManager.Instance.ResetScore();

        //TutorialUI.Instance.ResetSelf();
    }

    public void PlayAgain()
    {
        state = GameState.WaittingToStart;

        GameReset();
    }

    public void BackToMainMenu()
    {
        Loader.LoadScene(Loader.Scene.MainMenu);
    }
}
