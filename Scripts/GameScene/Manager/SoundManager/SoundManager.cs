using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClipSO audioClipSO;

    [SerializeField] private AudioSource musicAudioSource;

    private float soundEffectVolume = 1f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DeliveryManager.Instance.OnDeliveryCompletedPlaySound += DeliveryManager_OnDeliveryCompletedPlaySound;
        DeliveryManager.Instance.OnDeliveryFailedPlaySound += DeliveryManager_OnDeliveryFailedPlaySound;
        CuttingCounter.OnCuttingPlaySound += CuttingCounter_OnCuttingPlaySound;
        PlayerInteract.Instance.OnPlayerGrabSomethingPlaySound += PlayerInteract_OnPlayerGrabSomethingPlaySound;
        BaseCounter.OnKitchenObjectDropedPlaySound += BaseCounter_OnKitchenObjectDropedPlaySound;
        TrashCounter.OnKitchenObjectTrashedPlaySound += TrashCounter_OnKitchenObjectTrashedPlaySound;
        PlayerSound.OnMovingPlaySound += PlayFootStepSound;

    }

    public void ChangeSoundEffectVolume(float volume)
    {
        soundEffectVolume = volume;
    }

    public float GetSoundEffectVolume()
    {
        return soundEffectVolume;
    }

    public void ChangeMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;
    }

    public float GetMusicVolume()
    {
        return musicAudioSource.volume;
    }

    private void TrashCounter_OnKitchenObjectTrashedPlaySound()
    {
        PlaySoundEffect(audioClipSO.Trash, Camera.main.transform.position);
    }
    private void BaseCounter_OnKitchenObjectDropedPlaySound()
    {
        PlaySoundEffect(audioClipSO.ObjectDrop, Camera.main.transform.position);
    }
    private void PlayerInteract_OnPlayerGrabSomethingPlaySound()
    {
        PlaySoundEffect(audioClipSO.ObjectPickUp, Camera.main.transform.position);
    }
    private void CuttingCounter_OnCuttingPlaySound()
    {
        PlaySoundEffect(audioClipSO.Chop, Camera.main.transform.position);
    }
    private void DeliveryManager_OnDeliveryCompletedPlaySound()
    {
        PlaySoundEffect(audioClipSO.DeliverySuccess, Camera.main.transform.position);
    }

    private void DeliveryManager_OnDeliveryFailedPlaySound()
    {
        PlaySoundEffect(audioClipSO.DeliveryFail, Camera.main.transform.position);
    }

    private void PlaySoundEffect(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySoundEffect(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume * soundEffectVolume);
    }

    private void PlaySoundEffect(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume * soundEffectVolume);
    }

    /// <summary>
    /// play sound when player moving
    /// </summary>
    public void PlayFootStepSound()
    {
        PlaySoundEffect(audioClipSO.FootStep, Camera.main.transform.position);
    }

    public void PlayWarningSound()
    {
        PlaySoundEffect(audioClipSO.Warning, Camera.main.transform.position);
    }


}
