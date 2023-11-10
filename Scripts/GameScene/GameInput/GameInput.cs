using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    /// <summary>
    /// Singleton pattern
    /// </summary>
    public static GameInput Instance;

    public PlayerInputActions playerInputActions;

    /// <summary>
    /// Interact Action
    /// </summary>
    public Action<UnityEngine.InputSystem.InputAction.CallbackContext> OnInteractAction;

    /// <summary>
    /// Interact Alternate Action
    /// </summary>
    public Action<UnityEngine.InputSystem.InputAction.CallbackContext> OnInteractAlternateAction;

    public Action OnGamePaused;

    private const string PLAYER_PREFS_BINDINGS = "InputBindings";

    public enum KeyBinding
    {
        Up,
        Down,
        Left,
        Right,
        Interace,
        Alternate
    }


    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();

        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }


    /// <summary>
    /// when ESC pressed
    /// </summary>
    /// <param name="obj"></param>
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnGamePaused?.Invoke();
    }

    /// <summary>
    /// when F pressed
    /// </summary>
    /// <param name="obj"></param>
    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(obj);
    }

    /// <summary>
    /// when E pressed
    /// </summary>
    /// <param name="obj"></param>
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(obj);
    }

    public Vector3 GetMovementVectorNormalized()
    {
        Vector2 inputVec2 = playerInputActions.Player.Move.ReadValue<Vector2>();

        Vector2 moveDir = inputVec2.normalized;

        return new Vector3(moveDir.x, 0, moveDir.y);
    }

    /// <summary>
    /// key rebinging
    /// </summary>
    public void KeyReBinding(KeyBinding keyBinding, Action onCompleted)
    {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int rebindingIndex = -1;

        switch (keyBinding)
        {
            case KeyBinding.Up:
                inputAction = playerInputActions.Player.Move;
                rebindingIndex = 1;
                break;

            case KeyBinding.Down:
                inputAction = playerInputActions.Player.Move;
                rebindingIndex = 2;
                break;

            case KeyBinding.Left:
                inputAction = playerInputActions.Player.Move;
                rebindingIndex = 3;
                break;

            case KeyBinding.Right:
                inputAction = playerInputActions.Player.Move;
                rebindingIndex = 4;
                break;

            case KeyBinding.Interace:
                inputAction = playerInputActions.Player.Interact;
                rebindingIndex = 0;
                break;

            case KeyBinding.Alternate:
                inputAction = playerInputActions.Player.InteractAlternate;
                rebindingIndex = 0;
                break;

            default:
                inputAction = new InputAction();
                break;
        }

        inputAction?.PerformInteractiveRebinding(rebindingIndex)
                    .OnComplete(callback =>
                    {
                        callback.Dispose();
                        playerInputActions.Player.Enable();

                        PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
                        PlayerPrefs.Save();
                        onCompleted();
                    })
                    .Start();

    }

    public string GetKeyRebindingString(KeyBinding keyBinding)
    {
        string keyName = string.Empty;

        switch (keyBinding)
        {
            case KeyBinding.Up:
                keyName = playerInputActions.Player.Move.bindings[1].ToDisplayString();
                break;
            case KeyBinding.Down:
                keyName = playerInputActions.Player.Move.bindings[2].ToDisplayString();
                break;
            case KeyBinding.Left:
                keyName = playerInputActions.Player.Move.bindings[3].ToDisplayString();
                break;
            case KeyBinding.Right:
                keyName = playerInputActions.Player.Move.bindings[4].ToDisplayString();
                break;
            case KeyBinding.Interace:
                keyName = playerInputActions.Player.Interact.bindings[0].ToDisplayString();
                break;
            case KeyBinding.Alternate:
                keyName = playerInputActions.Player.InteractAlternate.bindings[0].ToDisplayString();
                break;
        }

        return keyName;
    }
}
