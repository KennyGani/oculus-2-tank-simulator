using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITankMovementComponent
{
    public void Enable();
    public void Disable();
    public void SetSpeed(float speed);
    public void SetTransformPathSource(string transformPathFilename);
}
