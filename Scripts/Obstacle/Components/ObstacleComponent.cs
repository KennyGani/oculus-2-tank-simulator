using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ObstacleComponent : MonoBehaviour
{
    [SerializeField]
    private ObstacleCheckPointComponent[] obstacleCheckPointComponents;

    [SerializeField]
    private ObstacleShootingIndicatorComponent obstacleShootingIndicatorComponent;

    private Subject<ObstacleName> obstacleCheckpointReachedSubject;
    private Subject<Unit> obstacleShootingIndicatorCompletionSubject;

    public IObservable<ObstacleName> ObstacleCheckpointReachedObservable
    {
        get
        {
            return this.obstacleCheckpointReachedSubject.AsObservable();
        }
    }

    public IObservable<Unit> ObstacleShootingIndicatorCompletionObservable
    {
        get
        {
            return this.obstacleShootingIndicatorCompletionSubject.AsObservable();
        }
    }

    ObstacleComponent()
    {
        this.obstacleCheckpointReachedSubject = new Subject<ObstacleName>();
        this.obstacleShootingIndicatorCompletionSubject = new Subject<Unit>();
    }

    void Awake()
    {
        this.obstacleCheckPointComponents = this.gameObject.GetComponentsInChildren<ObstacleCheckPointComponent>();
        this.obstacleShootingIndicatorComponent = this.gameObject.GetComponentInChildren<ObstacleShootingIndicatorComponent>();
    }

    void Start()
    {
        foreach (var component in this.obstacleCheckPointComponents)
        {
            component.obstacleCheckPointComponentObservable.Subscribe(this.obstacleCheckpointReachedSubject.OnNext);
        }

        this.obstacleShootingIndicatorComponent.ObstacleShootingIndicatorComponentObservable.Subscribe(this.obstacleShootingIndicatorCompletionSubject.OnNext);
    }
}
