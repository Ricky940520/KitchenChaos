using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Player's interaction
/// </summary>
public class PlayerInteract : MonoBehaviour, ISetKitchenObject
{
    public static PlayerInteract Instance { get; private set; }

    /// <summary>
    /// Used for Physics.Raycast
    /// </summary>
    [SerializeField] private LayerMask counters;

    /// <summary>
    /// Ray's distance for raycast
    /// </summary>
    [SerializeField] private float rayDistance = 1.5f;

    /// <summary>
    /// when player has selected the counter
    /// </summary>
    private BaseCounter selectedCounter = null;

    /// <summary>
    /// Event of when Selected Counter Changed
    /// </summary>
    public Action<BaseCounter> OnSelectedCounterChanged;

    /// <summary>
    /// a point of kitchen object when the player has held it
    /// </summary>
    [SerializeField] private Transform kitchenHoldPoint;

    /// <summary>
    /// kitchen object data when the player has held it 
    /// </summary>
    private KitchenObject kitchenObject = null;

    /// <summary>
    /// play Sound Effect
    /// </summary>
    public Action OnPlayerGrabSomethingPlaySound;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameInput.Instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    /// <summary>
    /// when E pressed 
    /// </summary>
    /// <param name="context"></param>
    private void GameInput_OnInteractAction(InputAction.CallbackContext context)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    /// <summary>
    /// when F pressed
    /// </summary>
    private void GameInput_OnInteractAlternateAction(InputAction.CallbackContext context)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, rayDistance, counters))
        {
            if (hitInfo.transform.TryGetComponent(out BaseCounter currentBaseCounter))
            {
                if (currentBaseCounter != selectedCounter)
                {
                    SetSelectedCounter(currentBaseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(selectedCounter);
    }

    /// <summary>
    /// Drop kitchen object to player
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="target">where you want to drop</param>
    /// <param name="kitchenObject"></param>
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        this.kitchenObject.transform.SetParent(kitchenHoldPoint, false);
        this.kitchenObject.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        selectedCounter.SetKitchenObjectToNull();

        OnPlayerGrabSomethingPlaySound?.Invoke();
    }

    /// <summary>
    /// when player has kitchen object return true otherwise false
    /// </summary>
    /// <returns></returns>
    public bool PlayerHasKitchenObject()
    {
        return kitchenObject != null;
    }


    public Transform GetKitchenHoldPoint()
    {
        return kitchenHoldPoint;
    }

    public KitchenObject GetPlayerKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObjectToNull()
    {
        kitchenObject = null;
    }
}
