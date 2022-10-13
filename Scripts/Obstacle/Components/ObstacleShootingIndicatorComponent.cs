using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ObstacleShootingIndicatorComponent : MonoBehaviour
{
    private bool mustDetectCollision = false;
    private Subject<Unit> ObstacleShootingIndicatorComponentSubject;

    public IObservable<Unit> ObstacleShootingIndicatorComponentObservable
    {
        get
        {
            return this.ObstacleShootingIndicatorComponentSubject.AsObservable();
        }
    }

    ObstacleShootingIndicatorComponent()
    {
        this.ObstacleShootingIndicatorComponentSubject = new Subject<Unit>();
    }

    void Start()
    {
        Invoke("EnableCollisionDetection", 5f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!this.mustDetectCollision)
        {
            return;
        }

        this.ObstacleShootingIndicatorComponentSubject.OnNext(Unit.Default);
    }

    private void EnableCollisionDetection()
    {
        this.mustDetectCollision = true;
    }
}
