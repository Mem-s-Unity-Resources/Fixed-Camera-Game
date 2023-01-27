using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_PlatformAnim : MonoBehaviour
{
    //This script triggers a single animation using Trigger Collider
    public Animator anim;//drag your animation object into this variable within unity inspector
    public float delayTime=0.1f;//Delay time in seconds gives the player a chance to get onto the platform
    Transform Player;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = other.transform;
            Player.SetParent(this.transform);

            if (anim.GetBool("active"))
            {
                StartCoroutine(Delay(false));

            }
            else
            {
                StartCoroutine(Delay(true));
            }
        }
    }


    void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            Player.SetParent(null);
        }
    }

    IEnumerator Delay(bool state) { 
      yield return new WaitForSeconds(delayTime);
      anim.SetBool("active", state);//Change the string "Active" to the name of your animator parameter (bool)
        
    }
}
