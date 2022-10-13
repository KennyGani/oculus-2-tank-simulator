using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTurretPeriscopeComponent : MonoBehaviour
{
    void Start()
    {
        this.Disable();
    }

    public void Enable()
    {
        this.gameObject.SetActive(true);
    }

    public void Disable()
    {
        this.gameObject.SetActive(false);
    }
}
