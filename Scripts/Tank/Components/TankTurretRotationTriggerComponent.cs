using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TankTurretRotationTriggerComponent : MonoBehaviour
{
    [SerializeField]
    private TankTurretRotationDirection tankTurretRotationDirection;
    private Subject<TankTurretRotationDirection> tankTurretTriggerRotationDirectionSubject;
    private Subject<Unit> tankTurretTriggerRotationStopSubject;

    public IObservable<TankTurretRotationDirection> TankTurretTriggerRotationDirectionObservable
    {
        get
        {
            return this.tankTurretTriggerRotationDirectionSubject.AsObservable();
        }
    }

    public IObservable<Unit> TankTurretTriggerRotationStopObservable
    {
        get
        {
            return this.tankTurretTriggerRotationStopSubject.AsObservable();
        }
    }

    TankTurretRotationTriggerComponent()
    {
        this.tankTurretTriggerRotationDirectionSubject = new Subject<TankTurretRotationDirection>();
        this.tankTurretTriggerRotationStopSubject = new Subject<Unit>();
    }

    public void OnCollisionStay(Collision collision)
    {
        this.tankTurretTriggerRotationDirectionSubject.OnNext(this.tankTurretRotationDirection);
    }

    public void OnCollisionExit(Collision collision)
    {
        this.tankTurretTriggerRotationStopSubject.OnNext(Unit.Default);
    }

}
