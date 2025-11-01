using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Hittable
{
    float GetVibrationFrequency();
    float GetVibrationDuration();
    float GetVibrationAmplitudeScale();
    void TipHit(float volume);
    void StickHit(float volume);

}