using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ResetView : MonoBehaviour
{
    public Transform defaultPosition;
    public Transform headPosition;

    private void Start()
    {
        ResetCameraPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions.drumGame.CenterCamera.GetStateDown(SteamVR_Input_Sources.Any))
        {
            ResetCameraPosition();
        }
    }

    void ResetCameraPosition(bool resetY = false)
    {
        Vector3 movement = defaultPosition.position - headPosition.position;
        float y_rotation = defaultPosition.eulerAngles.y - headPosition.eulerAngles.y;
        if (!resetY)
        {
            movement.y = 0;
        }
        this.transform.position = this.transform.position + movement;
        this.transform.RotateAround(headPosition.position, new Vector3(0, 1, 0), y_rotation);
    }
}
