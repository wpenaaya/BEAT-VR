using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrumScript : MonoBehaviour, Hittable
{
    public AudioClip tipHitSound;
    public AudioClip stickHitSound;
    public GameObject scorePopupPrefab;
    public Transform scorePopupTransform;

    private AudioSource audioSource;

    // Allows individual drums to have different haptics
    public float vibrationFrequency = 90;
    public float vibrationAmplitudeScale = 1.0f;
    public float vibrationDuration = 0.1f;

    public DrumInputs drumInput;
    public event Func<DrumInputs, HitScores> OnHit;

    public float GetVibrationFrequency() { return vibrationFrequency; }
    public float GetVibrationAmplitudeScale() { return vibrationAmplitudeScale; }
    public float GetVibrationDuration() { return vibrationDuration; }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void TipHit(float volume)
    {
        audioSource.PlayOneShot(tipHitSound, volume);
        HitScores hitscore = OnHit(drumInput);
        //Debug.Log("Event result received!: " + hitscore.ToString());
        ShowScorePopup(hitscore);
    }

    public void StickHit(float volume)
    {
        audioSource.PlayOneShot(stickHitSound, volume);
        HitScores hitscore = OnHit.Invoke(drumInput);
        //Debug.Log("Event result received!: " + hitscore.ToString());
        ShowScorePopup(hitscore);
    }

    public void ShowScorePopup(HitScores hitscore)
    {
        if (hitscore == HitScores.NONE)
        {
            return;
        }
        GameObject scorePopup = Instantiate(scorePopupPrefab, scorePopupTransform.position, scorePopupTransform.rotation);
        switch (hitscore)
        {
            case HitScores.PERFECT:
                scorePopup.GetComponent<TextMeshPro>().SetText("PERFECT");
                break;
            case HitScores.GREAT:
                scorePopup.GetComponent<TextMeshPro>().SetText("GREAT");
                break;
            case HitScores.OKAY:
                scorePopup.GetComponent<TextMeshPro>().SetText("OKAY");
                break;
            case HitScores.MISS:
                scorePopup.GetComponent<TextMeshPro>().SetText("MISS");
                break;
        }
    }
}
