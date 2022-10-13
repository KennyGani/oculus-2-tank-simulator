using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TankSeatingTeleportComponent : MonoBehaviour
{
    [SerializeField]
    private TankSeatingName seatingName;

    private Subject<TankSeatingName> tankTeleportSubject;

    public IObservable<TankSeatingName> TankTeleportObservable
    {
        get
        {
            return this.tankTeleportSubject.AsObservable();
        }
    }

    TankSeatingTeleportComponent()
    {
        this.tankTeleportSubject = new Subject<TankSeatingName>();
    }

    void OnCollisionEnter(Collision collision)
    {
        this.tankTeleportSubject.OnNext(this.seatingName);
    }

    public TankSeatingName GetTankSeatingName()
    {
        return this.seatingName;
    }

    public void Enable()
    {
        this.gameObject.SetActive(true);
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
