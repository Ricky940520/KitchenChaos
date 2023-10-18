using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    /// <summary>
    /// Singleton pattern
    /// </summary>
    public static GameInput Instance;

    private PlayerInputActions playerInputActions;

    /// <summary>
    /// Interact Action
    /// </summary>
    public Action<UnityEngine.InputSystem.InputAction.CallbackContext> OnInteractAction;

    /// <summary>
    /// Interact Alternate Action
    /// </summary>
    public Action<UnityEngine.InputSystem.InputAction.CallbackContext> OnInteractAlternateAction;

    public Action OnGamePaused;


    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();
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
}
