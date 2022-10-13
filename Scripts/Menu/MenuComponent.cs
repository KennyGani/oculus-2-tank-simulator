using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Video;

public class MenuComponent : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    private Subject<Unit> videoPlayerEndSubject;
    private MenuPlayerStandingComponent menuPlayerStandingComponent;
    private MenuStartGameConfirmationComponent menuStartGameConfirmationComponent;

    MenuComponent()
    {
        this.videoPlayerEndSubject = new Subject<Unit>();
    }

    void Awake()
    {
        this.menuStartGameConfirmationComponent = this.gameObject.GetComponentInChildren<MenuStartGameConfirmationComponent>();
        this.menuPlayerStandingComponent = this.gameObject.GetComponentInChildren<MenuPlayerStandingComponent>();
        this.videoPlayer = this.gameObject.GetComponentInChildren<VideoPlayer>(true);
    }

    void Start()
    {
        this.menuStartGameConfirmationComponent.Hide();
        this.videoPlayer.aspectRatio = VideoAspectRatio.FitInside;
        this.videoPlayer.loopPointReached += OnVideoEnd;
    }

    public IObservable<Unit> PlayAndWaitVideo()
    {
        this.videoPlayer.Play();
        return this.videoPlayerEndSubject.AsObservable();
    }

    public Transform getStandingTransform()
    {
        return this.menuPlayerStandingComponent.getStandingTransform();
    }

    public IObservable<Unit> ShowAndWaitStartGameConfirmation()
    {
        this.menuStartGameConfirmationComponent.Show();
        return this.menuStartGameConfirmationComponent.startGameConfirmationObservable;
    }

    public void HideMenu()
    {
        this.gameObject.SetActive(false);
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        this.videoPlayerEndSubject.OnNext(Unit.Default);
    }
}
