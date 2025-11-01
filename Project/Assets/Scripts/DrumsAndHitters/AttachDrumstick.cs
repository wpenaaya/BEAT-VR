using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachDrumstick : MonoBehaviour
{
    public Rigidbody target;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = target.transform.position;
        this.transform.rotation = target.transform.rotation;
        GetComponent<ConfigurableJoint>().connectedBody = target;
    }
}
