using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAnimationComponent : MonoBehaviour
{
    [SerializeField]
    private float blinkDelayInSeconds;

    [SerializeField]
    private float minLightIntensity;

    [SerializeField]
    private float maxLightIntensity;

    private Light lightComponent;

    private float timeElapsedInSeconds;

    private void Awake()
    {
        this.lightComponent = this.gameObject.GetComponent<Light>();
        this.lightComponent.intensity = this.maxLightIntensity;
    }

    private void Update()
    {
        this.timeElapsedInSeconds += Time.deltaTime;

        if (this.timeElapsedInSeconds < this.blinkDelayInSeconds)
        {
            return;
        }

        this.timeElapsedInSeconds = 0;
        this.ToggleLight();
    }

    public void ToggleLight()
    {
        if (this.lightComponent.intensity == this.minLightIntensity)
        {
            this.lightComponent.intensity = this.maxLightIntensity;
        }
        else if (this.lightComponent.intensity == this.maxLightIntensity)
        {
            this.lightComponent.intensity = this.minLightIntensity;
        }
    }
}
