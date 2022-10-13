using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAnimationComponent : MonoBehaviour
{
    private float gateSpeed = 10f;
    private bool hasGateAlreadyOpened = false;

    void Update()
    {
        if (this.hasGateAlreadyOpened && this.transform.eulerAngles.z > 270 || this.transform.eulerAngles.z == 0)
        {
            this.transform.Rotate(Vector3.back * Time.deltaTime * this.gateSpeed);
        }
    }

    public void OpenGate()
    {
        this.hasGateAlreadyOpened = true;
    }
}
