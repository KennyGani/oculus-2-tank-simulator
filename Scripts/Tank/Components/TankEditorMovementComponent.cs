using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TankEditorMovementComponent : MonoBehaviour, ITankMovementComponent
{
    [SerializeField]
    private float movementSpeed = 0f;

    [SerializeField]
    private float rotationSpeed = 0.05f;

    [SerializeField]
    private bool isEnabled = false;

    private string transformPathSource;

    private List<TankTransformPath> tankTransformPaths = new List<TankTransformPath>();

    public void Disable()
    {
        this.isEnabled = false;
    }

    public void Enable()
    {
        this.isEnabled = true;
    }

    public void SetSpeed(float speed)
    {
        this.movementSpeed = speed;
    }

    public void SetTransformPathSource(string fileName)
    {
        this.transformPathSource = fileName;
    }

    void Start()
    {
        tankTransformPaths = FileHandlerUtility.ReadListFromJSON<TankTransformPath>(transformPathSource);
    }

    void Update()
    {
        if (!this.isEnabled)
        {
            return;
        }

        if (Input.GetKey("j"))
        {
            this.SetSpeed(0.04f);
        }
        else if (Input.GetKey("k"))
        {
            this.SetSpeed(0.03f);
        }
        else if (Input.GetKey("l"))
        {
            this.SetSpeed(0f);
        }
        else
        {
            this.SetSpeed(0.02f);
        }

        if (Input.GetKey("p"))
        {
            this.rotationSpeed = 0.08f;
        }
        else
        {
            this.rotationSpeed = 0.05f;
        }

        // 
        if (Input.GetKey("w"))
        {
            this.transform.Translate(new Vector3(0, 0, this.movementSpeed));
            this.SaveTransformToJson("move", this.movementSpeed);
        }

        if (Input.GetKey("s"))
        {
            this.transform.Translate(new Vector3(0, 0, -this.movementSpeed));
            this.SaveTransformToJson("move", -this.movementSpeed);
        }

        if (Input.GetKey("a"))
        {
            this.transform.Rotate(new Vector3(0, -this.rotationSpeed, 0));
            this.SaveTransformToJson("turn", -this.rotationSpeed);
        }

        if (Input.GetKey("d"))
        {
            this.transform.Rotate(new Vector3(0, this.rotationSpeed, 0));
            this.SaveTransformToJson("turn", this.rotationSpeed);
        }
    }

    private void SaveTransformToJson(string movementName, float movementSpeed)
    {
        tankTransformPaths.Add(new TankTransformPath(movementName, movementSpeed));
        FileHandlerUtility.SaveToJSON<TankTransformPath>(tankTransformPaths, transformPathSource);
    }
}
