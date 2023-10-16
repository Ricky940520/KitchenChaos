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

    private void TrashCounter_OnKitchenObjectTrashedPlaySound()
    {
        PlaySoundEffect(audioClipSO.Trash, PlayerInteract.Instance.transform.position);
    }
    private void BaseCounter_OnKitchenObjectDropedPlaySound()
    {
        PlaySoundEffect(audioClipSO.ObjectDrop, PlayerInteract.Instance.transform.position);
    }
    private void PlayerInteract_OnPlayerGrabSomethingPlaySound()
    {
        PlaySoundEffect(audioClipSO.ObjectPickUp, PlayerInteract.Instance.transform.position);
    }
    private void CuttingCounter_OnCuttingPlaySound(CuttingCounter cuttingCounter)
    {
        PlaySoundEffect(audioClipSO.Chop, cuttingCounter.transform.position);
    }
    private void DeliveryManager_OnDeliveryCompletedPlaySound()
    {
        PlaySoundEffect(audioClipSO.DeliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    private void DeliveryManager_OnDeliveryFailedPlaySound()
    {
        PlaySoundEffect(audioClipSO.DeliveryFail, DeliveryCounter.Instance.transform.position);
    }

    private void PlaySoundEffect(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySoundEffect(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySoundEffect(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    /// <summary>
    /// play sound when player moving
    /// </summary>
    public void PlayFootStepSound()
    {
        PlaySoundEffect(audioClipSO.FootStep, PlayerMove.Instance.transform.position);
    }

    
}
