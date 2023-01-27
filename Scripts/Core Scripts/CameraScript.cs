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
        Debug.Log("HERE "+ target);
        thisCam = GetComponent<Camera>();
        thisListener = GetComponent<AudioListener>();
        Debug.Log(gameObject.name + " Cam is " + thisCam);
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
    //BELOW NOT IMPLEMENTED//
   public void updatePlayerCam() {
        StartCoroutine(UpdateActiveCamera(target.gameObject));
    }
    IEnumerator UpdateActiveCamera(GameObject player)
    { //No need to get the rigidbody controller //
        Rigidbody_Controller playerController = player.GetComponent<Rigidbody_Controller>();
        playerController.activeCamera = this.gameObject;
        yield return new WaitForSeconds(1);
        playerController.pointerCamera = transform.GetChild(0).GetComponent<Camera>();

    }
    //Function: updatePlayerCam and UpdateActiveCamera are only necessary if the player is using rotType.screenPos (rotation type  "Looking at cursor")
}
