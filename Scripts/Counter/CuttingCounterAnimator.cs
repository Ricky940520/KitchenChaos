using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterAnimator : MonoBehaviour
{
    private const string CUT = "Cut";

    /// <summary>
    /// cutting counter animator
    /// </summary>
    private Animator animator;

    [SerializeField] private CuttingCounter cuttingCounter;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnCutProcessCountChangedFireAnimator += CuttingCounter_OnCutProcessCountChangedFireAnimator;
    }

    private void CuttingCounter_OnCutProcessCountChangedFireAnimator()
    {
        animator.SetTrigger(CUT);
    }
}
