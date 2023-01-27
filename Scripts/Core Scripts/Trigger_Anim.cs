using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Anim : MonoBehaviour
{
    //This script triggers a single animation using Trigger Collider
    public Animator anim;//drag your animation object into this variable within unity inspector

    void OnTriggerEnter() {
        anim.SetBool("active",true);//Change the string "Active" to the name of your animator parameter (bool)
    }

    void OnTriggerExit() {
        anim.SetBool("active", false);

    }

}
