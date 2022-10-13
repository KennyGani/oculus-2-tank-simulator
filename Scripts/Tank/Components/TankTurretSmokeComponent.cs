using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankTurretSmokeComponent : MonoBehaviour
{
    private ParticleSystem smokeParticleSystem;

    void Awake()
    {
        this.smokeParticleSystem = this.gameObject.GetComponent<ParticleSystem>();
    }

    public void ShowOnce()
    {
        this.smokeParticleSystem.Play();
    }
}
