using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_AnimAddForce : MonoBehaviour
{
    //This script triggers a single animation using Trigger Collider
    public Animator anim;//drag your animation object into this variable within unity inspector
    Rigidbody_Controller Player;
    public float force = 1f;
    public float delay = 1f;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            anim.SetBool("active", true);//Change the string "Active" to the name of your animator parameter (bool)
            Player = other.gameObject.GetComponent<Rigidbody_Controller>();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Player.canMove = false;
            StartCoroutine(DelayFroce(Player));

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Player.canMove = true;
            anim.SetBool("active", false);
            Debug.Log("<color=red>Exited:" + other.tag + "</color>");
        }
    }

    IEnumerator DelayFroce(Rigidbody_Controller player)
    { 
        yield return new WaitForSeconds(delay);
        Player.rbody.AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
