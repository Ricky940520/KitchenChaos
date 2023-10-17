using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resume;
    [SerializeField] private Button mainMenu;

    private void Start()
    {
        KitchenChaosGameManager.Instance.OnGamePaused += KitchenChaosGameManager_OnGamePased;
        Hide();

        resume.onClick.AddListener(() =>
        {
            KitchenChaosGameManager.Instance.GameInput_OnGamePaused();
        });

        mainMenu.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scene.MainMenu);
        });
    }

    private void KitchenChaosGameManager_OnGamePased(bool isGamePasued)
    {
        if (isGamePasued)
        {
            Show();
        }
        else
        {
            Hide();
        }
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
