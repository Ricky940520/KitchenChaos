using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFinalScene : MonoBehaviour
{
    private bool isFirstFrame = true;

    private void Update()
    {
        if (isFirstFrame)
        {
            isFirstFrame = false;

            Loader.LoadScene(Loader.Scene.GameScene);
        }
    }
}
