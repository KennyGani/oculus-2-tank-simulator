using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class TankTurretTriggerComponent : MonoBehaviour
{
    private Subject<Unit> tankTurretTriggerSubject;

    public IObservable<Unit> TankTurretTriggerObservable
    {
        get
        {
            return this.tankTurretTriggerSubject.AsObservable();
        }
    }

    void Update()
    {
#if DEVELOPMENT
        if (Input.GetKey("q"))
        {
            this.tankTurretTriggerSubject.OnNext(Unit.Default);
        }
#endif
    }

    TankTurretTriggerComponent()
    {
        this.tankTurretTriggerSubject = new Subject<Unit>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        this.tankTurretTriggerSubject.OnNext(Unit.Default);
    }
}
