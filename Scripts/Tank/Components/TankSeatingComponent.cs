using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSeatingComponent : MonoBehaviour
{
    public Transform GetFrontRightSeatingTransform()
    {
        return this.gameObject.transform.GetChild(1);
    }

    public Transform GetRearLeftSeatingTransform()
    {
        return this.gameObject.transform.GetChild(2);
    }

    public Transform GetRearRightSeatingTransform()
    {
        return this.gameObject.transform.GetChild(0);
    }

    public Transform GetEditorSeatingTransform()
    {
        return this.gameObject.transform.GetChild(3);
    }
}
