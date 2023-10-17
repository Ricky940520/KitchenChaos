using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject[] selectedCounterGO;
    private BaseCounter selectedBaseCounter;


    private void Start()
    {
        selectedBaseCounter = this.GetComponent<BaseCounter>();
        PlayerInteract.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(BaseCounter selectedCounter)
    {
        if (selectedCounter == this.selectedBaseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }


    public void Show()
    {
        foreach (var go in selectedCounterGO)
        {
            go.SetActive(true);
        }

    }

    public void Hide()
    {
        foreach (var go in selectedCounterGO)
        {
            go.SetActive(false);
        }
    }
}
