using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKitchenObjectSO", menuName = "Custom/KitchenObjectSO")]
public class KitchenObjectSO : ScriptableObject
{
    public Transform Prefab;
    public Sprite Sprite;
    public string ObjectName;
}
