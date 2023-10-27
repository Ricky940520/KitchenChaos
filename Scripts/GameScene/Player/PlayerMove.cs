using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove Instance;

    private CharacterController characterController;

    /// <summary>
    /// move speed of player
    /// </summary>
    [SerializeField] private float moveSpeed = 5f;

    /// <summary>
    /// rotate speed of player
    /// </summary>
    [SerializeField] private float rotateSpeed = 10f;

    /// <summary>
    /// is player in controlled
    /// </summary>
    private bool isMoving = false;



    private void Awake()
    {
        Instance = this;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        if (KitchenChaosGameManager.Instance.GetGameState() != KitchenChaosGameManager.GameState.PlayingGame)
        {
            isMoving = false;
            return;
        }
        Vector3 inputVec3 = GameInput.Instance.GetMovementVectorNormalized();

        isMoving = (inputVec3 != Vector3.zero);

        if (isMoving)
        {
            this.transform.forward = Vector3.Slerp(transform.forward, inputVec3, rotateSpeed * Time.deltaTime);
            //this.transform.position += moveDir * moveSpeed * Time.deltaTime;

            characterController.Move(moveSpeed * Time.deltaTime * inputVec3);
        }
    }

    public bool IsWalking()
    {
        return isMoving;
    }


}
