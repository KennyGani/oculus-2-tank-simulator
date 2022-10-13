using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TankPathMovementComponent : MonoBehaviour, ITankMovementComponent
{
    [SerializeField]
    private float movementSpeed = 0f;

    [SerializeField]
    private bool isEnabled = false;

    [SerializeField]
    private int currentPathIndex;

    private bool isAlreadyMove = false;

    private string transformPathSource;

    private List<TankTransformPath> tankTransformPaths;

    private AudioSource audioSource;

    private Subject<TankMovementStatus> tankMovementStatusSubject;

    public IObservable<TankMovementStatus> TankMovementStatusObservable
    {
        get
        {
            return this.tankMovementStatusSubject.AsObservable();
        }
    }

    TankPathMovementComponent()
    {
        this.tankTransformPaths = new List<TankTransformPath>();
        this.tankMovementStatusSubject = new Subject<TankMovementStatus>();
    }

    public void Disable()
    {
        this.isEnabled = false;
        this.audioSource.Pause();
        this.tankMovementStatusSubject.OnNext(TankMovementStatus.Stop);
    }

    public void Enable()
    {
        this.isEnabled = true;
        this.audioSource.Play();
        this.tankMovementStatusSubject.OnNext(TankMovementStatus.Move);
    }

    public void SetSpeed(float speed)
    {
        this.movementSpeed = speed;
    }

    public void SetTransformPathSource(string filename)
    {
        this.transformPathSource = filename;
    }

    void Awake()
    {
        this.audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (this.isEnabled && !this.isAlreadyMove)
        {
            this.isAlreadyMove = true;
            this.StartCoroutine(this.StartTankMovement());
        }

#if DEVELOPMENT
        if (Input.GetKey("p"))
        {
            this.PauseTankMovement();
        }

        if (Input.GetKey("s"))
        {
            this.UnpauseTankMovement();
        }
#endif
    }

    public IEnumerator StartTankMovement()
    {

        this.tankTransformPaths = FileHandlerUtility.ReadListFromJSON<TankTransformPath>(transformPathSource);

        for (int i = currentPathIndex; i < this.tankTransformPaths.Count; i++)
        {
            yield return new WaitUntil(() => this.isEnabled == true);

            if (tankTransformPaths[i].PathTransformName == "move")
            {
                this.transform.Translate(new Vector3(0, 0, this.tankTransformPaths[i].SpeedTransform));
            }

            if (tankTransformPaths[i].PathTransformName == "turn")
            {
                this.transform.Rotate(new Vector3(0, this.tankTransformPaths[i].SpeedTransform, 0));
            }

            SetSpeed(this.tankTransformPaths[i].SpeedTransform);

            currentPathIndex = i;
        }
    }
}
