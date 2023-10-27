using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterWarningImage : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private ProgressBar progressBar;

    private const string IS_FLASHING = "IsFlashing";

    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        progressBar.OnWarning += ProgressBar_OnWarning;
        progressBar.OnIdle += ProgressBar_OnIdle;

        Hide();
    }

    private void ProgressBar_OnWarning()
    {
        if (stoveCounter.IsFried())
        {
            Show();

            animator.SetBool(IS_FLASHING, true);
        }
    }

    private void ProgressBar_OnIdle()
    {
        Hide();

        animator.SetBool(IS_FLASHING, false);
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
