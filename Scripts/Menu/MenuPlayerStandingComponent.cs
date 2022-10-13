using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayerStandingComponent : MonoBehaviour
{
    public Transform getStandingTransform()
    {
        return this.gameObject.transform;
    }
}
