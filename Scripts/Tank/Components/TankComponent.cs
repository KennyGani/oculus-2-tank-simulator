using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TankComponent : MonoBehaviour
{
    private ITankMovementComponent tankMovementComponent;
    private ITankTurretComponent tankTurretComponent;
    private Dictionary<TankSeatingName, Transform> tankSeatingTransformDictionary;
    private TankSeatingTeleportComponent[] tankSeatingTeleportComponents;
    private TankStartMovementTriggerComponent tankStartMovementTriggerComponent;
    private Subject<TankSeatingName> tankSeatingTeleportationSubject;
    private Subject<Unit> tankStartMovementTriggerSubject;

    public IObservable<TankSeatingName> TankSeatingTeleportationObservable
    {
        get
        {
            return this.tankSeatingTeleportationSubject.AsObservable();
        }
    }

    public IObservable<Unit> TankStartMovementTriggerObservable
    {
        get
        {
            return this.tankStartMovementTriggerSubject.AsObservable();
        }
    }

    TankComponent()
    {
        this.tankSeatingTransformDictionary = new Dictionary<TankSeatingName, Transform>();
        this.tankSeatingTeleportationSubject = new Subject<TankSeatingName>();
        this.tankStartMovementTriggerSubject = new Subject<Unit>();
    }

    void Awake()
    {
        this.tankSeatingTeleportComponents = this.gameObject.GetComponentsInChildren<TankSeatingTeleportComponent>();
        this.tankStartMovementTriggerComponent = this.gameObject.GetComponentInChildren<TankStartMovementTriggerComponent>();
    }

    void Start()
    {
        foreach (var tankSeatingTeleportComponent in tankSeatingTeleportComponents)
        {
            tankSeatingTeleportComponent.TankTeleportObservable.Subscribe((tankSeatingName) =>
            {
                this.tankSeatingTeleportationSubject.OnNext(tankSeatingName);
                this.DisableSeatingTargetAndEnablePossibleSeatings(tankSeatingName);
            });
        }

        tankStartMovementTriggerComponent.TankStartMovementTriggerObservable.Subscribe(_ =>
        {
            this.tankStartMovementTriggerSubject.OnNext(Unit.Default);
            this.tankStartMovementTriggerComponent.Hide();
        });
    }

    public void SetMovementComponent(ITankMovementComponent tankMovementComponent)
    {
        this.tankMovementComponent = tankMovementComponent;
    }

    public void EnableTankMovement()
    {
        this.tankMovementComponent.Enable();
    }

    public void DisableTankMovement()
    {
        this.tankMovementComponent.Disable();
    }

    public void SetTankMovementSpeed(float speed)
    {
        this.tankMovementComponent.SetSpeed(speed);
    }

    public void SetTankMovementSourcePathFileName(string transformPathSource)
    {
        this.tankMovementComponent.SetTransformPathSource(transformPathSource);
    }

    public void SetSeatingTransform(TankSeatingName seatingName, Transform seatingTransform)
    {
        this.tankSeatingTransformDictionary.Add(seatingName, seatingTransform);
    }

    public Transform GetSeatingTransformForSeatingName(TankSeatingName seatingName)
    {
        return this.tankSeatingTransformDictionary.GetValueOrDefault(seatingName);
    }

    public void SetTankTurretComponent(TankTurretComponent tankTurretComponent)
    {
        this.tankTurretComponent = tankTurretComponent;
    }

    public void RotateTurretLeft()
    {
        this.tankTurretComponent.StartRotateLeft();
    }

    public void RotateTurretRight()
    {
        this.tankTurretComponent.StartRotateRight();
    }

    public void StopTurretRotation()
    {
        this.tankTurretComponent.StopRotation();
    }

    public void TurretShoot()
    {
        this.tankTurretComponent.ShootBullet();
    }

    public void EnableTurretPeriscopeView()
    {
        this.tankTurretComponent.EnableTurretPeriscopeView();
    }

    public void DisableTurretPeriscopeView()
    {
        this.tankTurretComponent.DisableTurretPeriscopeView();
    }

    private void DisableSeatingTargetAndEnablePossibleSeatings(TankSeatingName tankSeatingName)
    {
        foreach (var tankSeatingTeleportComponent in tankSeatingTeleportComponents)
        {
            if (tankSeatingTeleportComponent.GetTankSeatingName() == tankSeatingName)
            {
                tankSeatingTeleportComponent.Disable();
                continue;
            }

            tankSeatingTeleportComponent.Enable();
        }
    }
}
