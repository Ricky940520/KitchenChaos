using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] GameObject[] stoveCounterVisual;

    private void Start()
    {
        stoveCounter.OnIsFryingChanged += StoveCounter_OnIsFryingChanged;
    }

    /// <summary>
    /// Stove Counter Visual Show or Hide 
    /// </summary>
    /// <param name="isFrying"></param>
    private void StoveCounter_OnIsFryingChanged(bool isFrying)
    {
        foreach (var item in stoveCounterVisual)
        {
            item.SetActive(isFrying);
        }
    }
}
