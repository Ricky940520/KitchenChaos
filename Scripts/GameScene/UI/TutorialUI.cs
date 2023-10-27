using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    public static TutorialUI Instance { get; private set; }

    public Action OnTutorialHide;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Show();

        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;


    }

    private void GameInput_OnInteractAction(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
    {
        if (KitchenChaosGameManager.Instance.GetGameState() == KitchenChaosGameManager.GameState.WaittingToPressInteract)
        {
            Hide();
            OnTutorialHide?.Invoke();
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
