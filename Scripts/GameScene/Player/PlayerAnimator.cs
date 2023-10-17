using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    /// <summary>
    /// animator parameters
    /// </summary>
    private const string Is_Walking = "IsWalking";

    private Animator animator;

    private PlayerMove player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GetComponentInParent<PlayerMove>();
    }

    private void LateUpdate()
    {
        animator.SetBool(Is_Walking, player.IsWalking());
    }
}
