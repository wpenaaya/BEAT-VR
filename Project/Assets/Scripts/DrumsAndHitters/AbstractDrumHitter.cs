using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public abstract class AbstractDrumHitter : MonoBehaviour
{
    public SteamVR_Input_Sources inputSource;
    public SteamVR_Action_Vibration haptics;

    public Transform tip; // For velocity
    public float minimumVelocity = 0.1f;
    public float maximumVelocity = 2.0f;

    public float vibrationIntensityScale = 1.0f;

    private Vector3 oldTipPosition;
    private float tipSpeed;

    private void Start()
    {
        oldTipPosition = tip.position;
        tipSpeed = 0;
    }

    void FixedUpdate()
    {
        tipSpeed = ((tip.position - oldTipPosition) / Time.deltaTime).magnitude;
        oldTipPosition = tip.position;
    }

    protected float CalculateHitVolume()
    {
        // Debug.Log("tipSpeed: " + tipSpeed);
        float result;
        if(tipSpeed < minimumVelocity)
        {
            result = 0.0f;
        } else if (tipSpeed > maximumVelocity)
        {
            result = 1.0f;
        } else
        {
            result = (tipSpeed - minimumVelocity) / (maximumVelocity - minimumVelocity);
        }
        // Debug.Log("Returning: " + result);
        return result;
    }

    private void OnTriggerEnter(Collider other)
    {
        Hittable drum = other.GetComponent<Hittable>();
        if (drum != null)
        {
            float volume = CalculateHitVolume();
            ExecuteHaptics(drum, volume);
            Hit(drum, volume);
        }

    }

    private void ExecuteHaptics(Hittable hittable, float volume)
    {
        haptics.Execute(
            0,
            hittable.GetVibrationDuration(),
            hittable.GetVibrationFrequency(),
            hittable.GetVibrationAmplitudeScale() * vibrationIntensityScale * volume,
            inputSource);
    }

    protected abstract void Hit(Hittable drum, float volume);
}