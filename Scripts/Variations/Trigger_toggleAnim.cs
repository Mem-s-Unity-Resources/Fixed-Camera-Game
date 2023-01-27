using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_toggleAnim : MonoBehaviour
{
    //This script triggers a single animation using Trigger Collider
    public Animator anim;//drag your animation object into this variable within unity inspector
    void OnTriggerEnter()
    {
        if (anim.GetBool("active"))
        {
            anim.SetBool("active", false);
        }
        else {
            anim.SetBool("active", true);
        }

    }
}

