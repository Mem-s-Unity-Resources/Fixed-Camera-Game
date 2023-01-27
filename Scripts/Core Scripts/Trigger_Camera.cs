using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Camera : MonoBehaviour
{
    public GameObject Camera;
    CameraScript camScript;

    private void Awake()
    {
        camScript = Camera.GetComponent<CameraScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            camScript.active = true;
            camScript.updatePlayerCam(); //Only if player's rotation type is screenPos
        }
        Debug.Log(gameObject.name+"Enter" + other.tag);
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            camScript.active = false;
        }
        Debug.Log(gameObject.name + "Exit" + other.tag);
    }

 
}
