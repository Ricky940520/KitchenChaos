using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    /// <summary>
    /// cutting process bar image UI 
    /// </summary>
    private Image cuttingProcessBar;

    [SerializeField] private BaseCounter counter;
    private IHasProgressBar progressBar;

    [SerializeField] private KitchenObjectSO meatPattyCooked;

    public Action OnWarning;
    public Action OnIdle;


    private void Awake()
    {
        cuttingProcessBar = GetComponent<Image>();
        progressBar = counter.GetComponent<IHasProgressBar>();

    }

    private void Start()
    {
        progressBar.OnProgressBarChanged += IHasProgressBar_OnProgressBarChanged;

        Hide();
    }

    /// <summary>
    /// set cutting process bar image percent.
    /// hide it when cuttingProcessPercent equal 1
    /// </summary>
    /// <param name="cuttingProcessPercent"></param>
    private void IHasProgressBar_OnProgressBarChanged(float cuttingProcessPercent)
    {
        if (cuttingProcessPercent != 1)
        {
            Show();
            cuttingProcessBar.fillAmount = cuttingProcessPercent;
        }
        else
        {
            cuttingProcessBar.fillAmount = cuttingProcessPercent;
            Hide();
            //if (counter is StoveCounter)
            //    OnIdle?.Invoke();
        }

        if (cuttingProcessPercent > 0.5f && cuttingProcessPercent < 1)
        {
            if (counter is StoveCounter)
                OnWarning?.Invoke();
        }
        else
        {
            if (counter is StoveCounter)
                OnIdle?.Invoke();
        }
    }

    private void Hide()
    {
        this.transform.parent.gameObject.SetActive(false);
    }

    private void Show()
    {
        this.transform.parent.gameObject.SetActive(true);
    }



}
