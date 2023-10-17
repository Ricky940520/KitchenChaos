using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllStatic : MonoBehaviour
{
    private void Awake()
    {
        BaseCounter.ResetAll();
        TrashCounter.ResetAll();
        CuttingCounter.ResetAll();
        PlayerSound.ResetAll();


    }
}
