using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentComponent : MonoBehaviour
{
    [SerializeField]
    private GateAnimationComponent gateAnimationComponent;

    void Awake()
    {
        this.gateAnimationComponent = this.GetComponentInChildren<GateAnimationComponent>();
    }

    public void OpenGate()
    {
        this.gateAnimationComponent.OpenGate();
    }
}
