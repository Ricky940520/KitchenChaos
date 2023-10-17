using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button play;
    [SerializeField] private Button quit;

    private void Start()
    {
        Time.timeScale = 1;

        play.onClick.AddListener(() =>
        {
            //Loading Scene
            Loader.LoadScene(Loader.Scene.Loading);
        });

        quit.onClick.AddListener(() =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
        });
    }
}
