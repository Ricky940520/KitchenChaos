using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryPopupUI : MonoBehaviour
{
    [SerializeField] private Color tickColor;
    [SerializeField] private Color crossColor;

    [SerializeField] private Sprite tickSprite;
    [SerializeField] private Sprite crossSprite;

    private const string TICK_TEXT = "Order\nCompleted";
    private const string CORSS_TEXT = "Order\nFailed";

    [SerializeField] private Image background;
    [SerializeField] private Image tickOrCorss;
    [SerializeField] private TextMeshProUGUI completedOrFailed;

    [SerializeField] private Animator animator;
    private const string ON_ACTION = "OnAction";

    private bool isShowing = false;
    private float showingTimer = 0f;
    private float showingTimerMax = 2f;

    private void Start()
    {
        DeliveryManager.Instance.OnDeliveryCompleted += DeliveryManager_OnDeliveryCompleted;
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;

        Hide();
    }

    private void Update()
    {
        if (isShowing)
        {
            showingTimer += Time.deltaTime;

            if(showingTimer>=showingTimerMax)
            {
                showingTimer = 0f;
                Hide();
            }
        }
    }

    private void DeliveryManager_OnDeliveryCompleted()
    {
        background.color = tickColor;
        tickOrCorss.sprite = tickSprite;
        completedOrFailed.text = TICK_TEXT;

        Show();
    }

    private void DeliveryManager_OnDeliveryFailed()
    {
        background.color = crossColor;
        tickOrCorss.sprite = crossSprite;
        completedOrFailed.text = CORSS_TEXT;

        Show();
    }


    public void Show()
    {
        this.gameObject.SetActive(true);
        animator.SetTrigger(ON_ACTION);
        isShowing = true;
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

}
