using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStateMachine : MonoBehaviour
{

    public enum animstate { idle, attack, dead }; //Rotation Type
    public animstate currState;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currState)
        {
            case animstate.idle:
                //Does anything happen at idle?
                break;
            case animstate.attack:
                anim.SetTrigger("attack");
                break;
            case animstate.dead:
                anim.SetTrigger("dead");
                break;
        }
    }

    void OnTriggerStay()
    {
        if (Input.GetKey(KeyCode.E))
        {
            currState = animstate.attack;
        }

    }

    private void OnTriggerExit()
    {
        if (currState == animstate.attack)
        {
            currState = animstate.dead;
        }

    }
}
