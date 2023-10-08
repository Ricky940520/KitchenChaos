using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum LookAtMode
    {
        CameraForward,
        CameraForwardInverted,
        None
    }

    [SerializeField] private LookAtMode lookAtMode;

    private void Start()
    {

    }

    private void LateUpdate()
    {
        switch (lookAtMode)
        {
            case LookAtMode.CameraForward:
                //this.transform.LookAt(Camera.main.transform);
                transform.forward = Camera.main.transform.forward;
                break;
            case LookAtMode.CameraForwardInverted:
                //this.transform.LookAt(-Camera.main.transform.position);
                transform.forward = -Camera.main.transform.forward;
                break;
        }

    }
}
