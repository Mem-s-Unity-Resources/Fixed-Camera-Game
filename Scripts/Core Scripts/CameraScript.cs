using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

   
    public Vector3 offset;
    Transform target;
    // Start is called before the first frame update
    public bool active=true;
    Camera thisCam;
    AudioListener thisListener;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        thisCam = GetComponent<Camera>();
        thisListener = GetComponent<AudioListener>();
        thisCam.enabled = active;
        gameObject.tag = active ? "ActiveCamera" : "Camera";

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) {
            Vector3 relativePos = (target.position + offset) - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }
        gameObject.tag = active ? "ActiveCamera" : "Camera";
        thisCam.enabled=active;
        thisListener.enabled = active;


    }
}
