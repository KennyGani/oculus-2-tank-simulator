using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialAnimationComponent : MonoBehaviour
{
    [SerializeField]
    private bool isEnabled = false;

    [SerializeField]
    private Color startColor;

    [SerializeField]
    private Color endColor;

    [SerializeField]
    [Range(0, 10)]
    private float blinkAnimationSpeed = 1;

    private Material material;

    void Awake()
    {

        this.material = this.gameObject.GetComponent<Material>();

        // Fallback 1
        if (!this.material)
        {
            this.material = this.gameObject.GetComponent<Renderer>().materials[0];
        }

        // Fallback 2
        if (!this.material)
        {
            this.material = this.gameObject.GetComponent<Renderer>().material;
        }
    }

    void Update()
    {
        if (!this.isEnabled)
        {
            return;
        }

        this.material.color = Color.Lerp(this.startColor, this.endColor, Mathf.PingPong(Time.time * this.blinkAnimationSpeed, 1));
    }

    public void Enable()
    {
        this.isEnabled = true;
    }

    public void Disable()
    {
        this.isEnabled = false;
    }
}
