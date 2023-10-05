using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingCounterBar : MonoBehaviour
{
    /// <summary>
    /// cutting process bar image UI 
    /// </summary>
    private Image cuttingProcessBar;

    [SerializeField] private CuttingCounter cuttingCounter;

    private void Awake()
    {
        cuttingProcessBar = GetComponent<Image>();
    }

    private void Start()
    {
        cuttingCounter.OnCutProcessCountChangedUpdateUI += CuttingCounter_OnCutProcessCountChanged;
        Hide();
    }

    /// <summary>
    /// set cutting process bar image percent
    /// </summary>
    /// <param name="cuttingProcessPercent"></param>
    private void CuttingCounter_OnCutProcessCountChanged(float cuttingProcessPercent)
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
