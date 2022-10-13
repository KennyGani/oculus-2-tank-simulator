using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.XR;

public class VrPlayerControllerComponent : MonoBehaviour
{
    private bool isMovingAnalog = false;

    private PlayerDeviceControllerActionType playerActionController;

    private Subject<PlayerDeviceControllerActionType> vrPlayerControllerComponentSubject;

    public IObservable<PlayerDeviceControllerActionType> VrPlayerControllerComponentObservable
    {
        get
        {
            return this.vrPlayerControllerComponentSubject.AsObservable();
        }
    }

    VrPlayerControllerComponent()
    {
        this.vrPlayerControllerComponentSubject = new Subject<PlayerDeviceControllerActionType>();
    }

    void Update()
    {
        if (this.isMovingAnalog && Input.GetAxis("XRI_Left_Primary2DAxis_Horizontal") == 0f && Input.GetAxis("XRI_Right_Primary2DAxis_Horizontal") == 0f)
        {
            this.vrPlayerControllerComponentSubject.OnNext(PlayerDeviceControllerActionType.AnalogAxisReset);
            this.isMovingAnalog = false;
        }

        if (Input.GetButtonDown("XRI_Left_TriggerButton") || Input.GetButtonDown("XRI_Right_TriggerButton"))
        {
            this.vrPlayerControllerComponentSubject.OnNext(PlayerDeviceControllerActionType.Trigger);
        }

        if (Input.GetAxis("XRI_Left_Primary2DAxis_Horizontal") < -0.5f || Input.GetAxis("XRI_Right_Primary2DAxis_Horizontal") < -0.5f)
        {
            this.isMovingAnalog = true;
            this.vrPlayerControllerComponentSubject.OnNext(PlayerDeviceControllerActionType.AnalogAxisLeft);
        }

        if (Input.GetAxis("XRI_Left_Primary2DAxis_Horizontal") > 0.5f || Input.GetAxis("XRI_Right_Primary2DAxis_Horizontal") > 0.5f)
        {
            this.isMovingAnalog = true;
            this.vrPlayerControllerComponentSubject.OnNext(PlayerDeviceControllerActionType.AnalogAxisRight);
        }
    }
}
