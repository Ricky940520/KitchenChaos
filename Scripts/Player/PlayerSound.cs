using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private float timerOfFootStepSound = 0;
    private float timerOfFootStepSoundMax = 1;

    public static Action OnMovingPlaySound;

    private void Update()
    {
        timerOfFootStepSound += Time.deltaTime;

        if (timerOfFootStepSound > timerOfFootStepSoundMax)
        {
            if (PlayerMove.Instance.IsWalking())
            {
                OnMovingPlaySound?.Invoke();
                timerOfFootStepSound = 0;
            }
        }
    }
}
