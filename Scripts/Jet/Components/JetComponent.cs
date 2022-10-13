using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class JetComponent : MonoBehaviour
{
    private bool isEnabled = false;
    private float movementSpeedMultiplier = 1f;
    private float movementSpeed = 0.6f;
    private AudioSource audioSource;

    void Awake()
    {
        this.audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!this.isEnabled)
        {
            return;
        }

        this.gameObject.transform.Translate(this.movementSpeed * this.movementSpeedMultiplier * this.transform.forward, Space.World);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    public void StartFlying(float movementSpeedMultiplier)
    {
        this.movementSpeedMultiplier = movementSpeedMultiplier;
        this.isEnabled = true;

        this.audioSource.volume = 0;

        StartCoroutine(this.StartJetAudioSource());

        StartCoroutine(this.IncreaseVolumeByTime());
    }

    public void StopFlying()
    {
        this.isEnabled = false;
    }

    private IEnumerator StartJetAudioSource()
    {
        yield return new WaitForSeconds(0f);
        this.audioSource.Play();
    }

    private IEnumerator IncreaseVolumeByTime()
    {
        yield return new WaitForSeconds(0.43f);

        this.audioSource.volume += 0.1f;

        if (this.audioSource.volume >= 1)
        {
            yield return null;
        }

        this.StartCoroutine(IncreaseVolumeByTime());
    }
}
