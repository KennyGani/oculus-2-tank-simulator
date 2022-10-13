using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class VrPlayerComponent : MonoBehaviour
{
    private VrPlayerControllerComponent vrPlayerControllerComponent;

    public IObservable<PlayerDeviceControllerActionType> PlayerDeviceControllerActionObservable
    {
        get
        {
            return this.playerDeviceControllerActionSubject;
        }
    }

    private Subject<PlayerDeviceControllerActionType> playerDeviceControllerActionSubject;

    VrPlayerComponent()
    {
        this.playerDeviceControllerActionSubject = new Subject<PlayerDeviceControllerActionType>();
    }

    void Awake()
    {
        this.vrPlayerControllerComponent = this.gameObject.GetComponent<VrPlayerControllerComponent>();
    }

    void Start()
    {
        this.vrPlayerControllerComponent.VrPlayerControllerComponentObservable.Subscribe(this.playerDeviceControllerActionSubject.OnNext);
    }

    public void ChangeParentTransform(Transform parentTransform)
    {
        this.gameObject.transform.SetParent(parentTransform, false);
    }

    public void ChangePlayerScale(float playerScaleMultiplier)
    {
        this.gameObject.transform.localScale *= playerScaleMultiplier;
    }
}
