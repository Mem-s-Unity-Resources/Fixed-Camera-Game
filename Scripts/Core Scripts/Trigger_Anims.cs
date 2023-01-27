using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Anims : MonoBehaviour
{
    //This script triggers multiple animations using Trigger Collider
    public Animator[] anims; //drag your animation objects into this variable within unity inspector
    void OnTriggerEnter()
    {
        foreach (Animator ani in anims)
        {
            ani.SetBool("active", true); //Change the string "Active" to the name of your animator parameter (bool)
        }
    }

    void OnTriggerExit()
    {
        foreach (Animator ani in anims)
        {
            ani.SetBool("active", false);  //Change the string "Active" to the name of your animator parameter (bool)
        }

    }
}
