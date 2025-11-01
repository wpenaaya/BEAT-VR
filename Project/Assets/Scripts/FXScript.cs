using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FXScript : MonoBehaviour
{
    public float pulseDuration = 0.4f;
    public float pulseIntensity = 0.5f;
    public float pulseBloomIntensity = 6f;
    public float pulseStep = 0.02f;
    public float fogFarDistance = 20f;
    public float fogCloseDistance = 4.5f;
    public float fogTransitionDuration = 1f;

    public Volume volume;
    private ChromaticAberration ca;
    private Bloom bloom;

    private float originalCAIntensity;
    private float originalBloomIntensity;

    private void Start()
    {
        RenderSettings.fogEndDistance = fogCloseDistance;
        ChromaticAberration caTmp;
        if (volume.profile.TryGet<ChromaticAberration>(out caTmp))
        {
            ca = caTmp;
            originalCAIntensity = (float) ca.intensity;
        }

        Bloom bloomTmp;
        if (volume.profile.TryGet<Bloom>(out bloomTmp))
        {
            bloom = bloomTmp;
            originalBloomIntensity = (float)bloom.intensity;
        }
    }

    public void FogClose()
    {
        StartCoroutine(FogCoroutine(fogFarDistance, fogCloseDistance));
    }

    public void FogFar()
    {
        StartCoroutine(FogCoroutine(fogCloseDistance, fogFarDistance));
    }

    IEnumerator FogCoroutine(float fogStart, float fogEnd)
    {
        int iterations = (int)(fogTransitionDuration / pulseStep);

        for (int i = 1; i <= iterations; i++)
        {
            float lerpValue = (float)i / iterations;
            lerpValue = easeInOutCubic(lerpValue);
            RenderSettings.fogEndDistance = Mathf.Lerp(fogStart, fogEnd, lerpValue);
            yield return new WaitForSeconds(pulseStep);
        }
    }

    public void PulseFX()
    {
        if (ca && bloom)
        {
            StartCoroutine(PulseFXCoroutine());
        }
    }

    IEnumerator PulseFXCoroutine()
    {
        int iterations = (int) (pulseDuration / pulseStep);
        
        /*
        for(int i = 1; i <= halfIterations; i++)
        {
            float lerpValue = (float) i / halfIterations;
            ca.intensity.Interp(originalCAIntensity, pulseIntensity, lerpValue);
            yield return new WaitForSeconds(pulseStep);
            Debug.Log("Intensity: " + ca.intensity);
        }
        */
        for (int i = 1; i <= iterations; i++)
        {
            float lerpValue = (float) i / iterations;
            lerpValue = easeInOutCubic(lerpValue);
            ca.intensity.Interp(pulseIntensity, originalCAIntensity, lerpValue);
            bloom.intensity.Interp(pulseBloomIntensity, originalBloomIntensity, lerpValue);
            yield return new WaitForSeconds(pulseStep);
        }
    }

    /*
     * CODE TAKEN DIRECTLY FROM:
     * https://easings.net/#easeInOutCubic
     */
    private static float easeInOutCubic(float x)
    {
        return (float) (x < 0.5 ? 4 * x * x * x : 1 - System.Math.Pow(-2 * x + 2, 3) / 2);
    }
}
