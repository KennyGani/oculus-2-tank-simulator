using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MenuStartGameConfirmationComponent : MonoBehaviour
{
    private Subject<Unit> startGameConfirmationSubject;

    public IObservable<Unit> startGameConfirmationObservable
    {
        get
        {
            return this.startGameConfirmationSubject.AsObservable();
        }
    }

    MenuStartGameConfirmationComponent()
    {
        this.startGameConfirmationSubject = new Subject<Unit>();
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void OnCollisionEnter(Collision collision)
    {
        this.startGameConfirmationSubject.OnNext(Unit.Default);
    }
}
