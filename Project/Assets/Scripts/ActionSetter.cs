using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ActionSetter : MonoBehaviour
{
    public SteamVR_ActionSet primarySet;

    // Start is called before the first frame update
    void Start()
    {
        if(primarySet != null)
        {
            primarySet.Activate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
