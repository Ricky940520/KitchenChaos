using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveSound : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnIsFryingChanged += PlayStoveSound;
    }

    /// <summary>
    /// play or pause sound
    /// </summary>
    /// <param name="isFrying"></param>
    public void PlayStoveSound(bool isFrying)
    {
        if (isFrying)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
