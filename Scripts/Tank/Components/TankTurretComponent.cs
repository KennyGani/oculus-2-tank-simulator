using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TankTurretComponent : MonoBehaviour, ITankTurretComponent
{
    [SerializeField]
    private bool isTurretRotationSoundPlaying = false;
    private float rotationInDegreesPerFrame = 0.25f;
    private bool canTurretShoot = true;
    private TankTurretTriggerComponent tankTurretTriggerComponent;
    private TankTurretRotationTriggerComponent[] tankTurretRotationTriggerComponents;
    private AudioSource tankTurretRotationAudioSource;
    private AudioSource tankTurretShootAudioSource;
    private TankBulletComponent tankBulletComponent;
    private TankTurretSmokeComponent tankTurretSmokeComponent;
    private TankTurretPeriscopeComponent tankTurretPeriscopeComponent;

    void Awake()
    {
        this.tankTurretTriggerComponent = this.gameObject.GetComponentInChildren<TankTurretTriggerComponent>();
        this.tankTurretSmokeComponent = this.gameObject.GetComponentInChildren<TankTurretSmokeComponent>();
        this.tankTurretRotationTriggerComponents = this.gameObject.GetComponentsInChildren<TankTurretRotationTriggerComponent>();
        this.tankBulletComponent = this.gameObject.GetComponentInChildren<TankBulletComponent>();
        this.tankTurretPeriscopeComponent = this.gameObject.GetComponentInChildren<TankTurretPeriscopeComponent>(true);
        var audioSources = this.gameObject.GetComponents<AudioSource>();
        this.tankTurretRotationAudioSource = audioSources[0];
        this.tankTurretShootAudioSource = audioSources[1];
    }

    void Start()
    {
        foreach (var tankTurretRotationTriggerComponent in this.tankTurretRotationTriggerComponents)
        {
            tankTurretRotationTriggerComponent.TankTurretTriggerRotationDirectionObservable.Subscribe(
                (TankTurretRotationDirection direction) =>
                {
                    if (direction == TankTurretRotationDirection.Left)
                    {
                        this.StartRotateLeft();
                    }

                    if (direction == TankTurretRotationDirection.Right)
                    {
                        this.StartRotateRight();
                    }
                });

            tankTurretRotationTriggerComponent.TankTurretTriggerRotationStopObservable.Subscribe(_ =>
            {
                this.StopTurretRotationSound();
            });
        }

        tankTurretTriggerComponent.TankTurretTriggerObservable.Subscribe(_ =>
        {
            this.ShootBullet();
        });
    }

    public void StartRotateRight()
    {
        this.PlayTurretRotationSound();
        this.gameObject.transform.Rotate(new Vector3(0, 0, rotationInDegreesPerFrame));
    }

    public void StartRotateLeft()
    {
        this.PlayTurretRotationSound();
        this.gameObject.transform.Rotate(new Vector3(0, 0, -rotationInDegreesPerFrame));
    }

    public void StopRotation()
    {
        this.StopTurretRotationSound();
    }

    public void ShootBullet()
    {
        if (!this.canTurretShoot)
        {
            return;
        }

        this.tankTurretShootAudioSource.Play();
        this.tankTurretSmokeComponent.ShowOnce();
        this.tankBulletComponent.SpawnBullet();
        this.canTurretShoot = false;
        Invoke("EnableTurretShoot", 6);
    }

    private void PlayTurretRotationSound()
    {
        if (this.isTurretRotationSoundPlaying)
        {
            return;
        }

        this.tankTurretRotationAudioSource.Play();
        this.isTurretRotationSoundPlaying = true;
    }

    public void StopTurretRotationSound()
    {
        this.tankTurretRotationAudioSource.Stop();
        isTurretRotationSoundPlaying = false;
    }

    private void EnableTurretShoot()
    {
        this.canTurretShoot = true;
    }

    public void EnableTurretPeriscopeView()
    {
        this.tankTurretPeriscopeComponent.Enable();
    }

    public void DisableTurretPeriscopeView()
    {
        this.tankTurretPeriscopeComponent.Disable();
    }
}
