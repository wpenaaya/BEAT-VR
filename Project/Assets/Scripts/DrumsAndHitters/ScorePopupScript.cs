using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePopupScript : MonoBehaviour
{
    public Vector3 floatPosition;
    public float lingerTime = 0.3f;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float timeAlive = 0.0f;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = transform.position + floatPosition;
        Destroy(gameObject, lingerTime);
    }
    void Update()
    {
        float lerpValue = timeAlive / lingerTime;

        this.transform.position = Vector3.Lerp(
            startPosition,
            endPosition,
            easeOutCirc(lerpValue) );

        FadeOut(easeInCubic(lerpValue));

        timeAlive += Time.deltaTime;
    }

    private static float easeOutCirc(float x) {
        return (float) System.Math.Sqrt(1 - System.Math.Pow(x - 1, 2));
    }

    private static float easeInCubic(float x)
    {
        return (float)x * x * x;
    }

    private void FadeOut(float lerpValue)
    {
        var material = GetComponent<TextMeshPro>();
        material.color = new Color(material.color.r, material.color.g, material.color.b, 1 - lerpValue);
    }
}
