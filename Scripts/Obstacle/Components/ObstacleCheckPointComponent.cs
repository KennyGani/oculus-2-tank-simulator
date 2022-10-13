using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ObstacleCheckPointComponent : MonoBehaviour
{
    [SerializeField]
    private ObstacleName obstacleName;

    private Subject<ObstacleName> obstacleCheckPointComponentSubject;

    public IObservable<ObstacleName> obstacleCheckPointComponentObservable
    {
        get
        {
            return this.obstacleCheckPointComponentSubject.AsObservable();
        }
    }

    ObstacleCheckPointComponent()
    {
        this.obstacleCheckPointComponentSubject = new Subject<ObstacleName>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "BulletPrefab(Clone)")
        {
            return;
        }

        this.obstacleCheckPointComponentSubject.OnNext(this.obstacleName);
        this.gameObject.SetActive(false);
    }
}
