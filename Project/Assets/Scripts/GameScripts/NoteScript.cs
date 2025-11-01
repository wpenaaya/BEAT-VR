using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteScript : MonoBehaviour
{
    public Vector3 start;
    public Vector3 end;
    public float duration;
    public float timeCreated;

    private void Start()
    {
        start = this.transform.position;
        timeCreated = (float) AudioSettings.dspTime;
    }
    // Update is called once per frame
    void Update()
    {
        float lerpValue = (float)(AudioSettings.dspTime - timeCreated) / duration;
        this.transform.position = Vector3.Lerp(
            start,
            end,
            lerpValue
            );
        if (lerpValue >= 1f)
        {
            Destroy(gameObject);
        }
    }
}
