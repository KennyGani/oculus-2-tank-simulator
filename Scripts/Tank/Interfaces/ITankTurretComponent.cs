using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITankTurretComponent
{
    public void StartRotateRight();
    public void StartRotateLeft();
    public void ShootBullet();
    public void StopRotation();
    public void EnableTurretPeriscopeView();
    public void DisableTurretPeriscopeView();
}
