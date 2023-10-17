using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAudioClipSO", menuName = "Custom/AudioClipSO")]
public class AudioClipSO : ScriptableObject
{
    public AudioClip[] Chop;
    public AudioClip[] DeliveryFail;
    public AudioClip[] DeliverySuccess;
    public AudioClip[] FootStep;
    public AudioClip[] ObjectDrop;
    public AudioClip[] ObjectPickUp;
    public AudioClip StoveSizzle;
    public AudioClip[] Trash;
    public AudioClip[] Warning;
}
