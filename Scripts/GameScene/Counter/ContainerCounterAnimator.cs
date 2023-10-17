using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterAnimator : MonoBehaviour
{
    /// <summary>
    /// ContainerCounter's animator
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Animator's trigger parameter
    /// </summary>
    private const string Open_Close = "OpenClose";

    /// <summary>
    /// ContainerCounter
    /// </summary>
    private ContainerCounter containerCounter;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        containerCounter = GetComponentInParent<ContainerCounter>();
    }

    private void Start()
    {
        containerCounter.OnContainerCounterInteractFireAnimator += OnContainerCounterInteractFireAnimator;
    }

    /// <summary>
    /// Fire the animator
    /// </summary>
    private void OnContainerCounterInteractFireAnimator()
    {
        animator.SetTrigger(Open_Close);
    }
}
