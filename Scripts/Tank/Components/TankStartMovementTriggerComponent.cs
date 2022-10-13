using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TankStartMovementTriggerComponent : MonoBehaviour
{
    private Subject<Unit> tankStartMovementTriggerSubject;

    public IObservable<Unit> TankStartMovementTriggerObservable
    {
        get
        {
            return this.tankStartMovementTriggerSubject.AsObservable();
        }
    }

    TankStartMovementTriggerComponent()
    {
        this.tankStartMovementTriggerSubject = new Subject<Unit>();
    }

    void OnCollisionEnter(Collision collision)
    {
        this.tankStartMovementTriggerSubject.OnNext(Unit.Default);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }
}
