using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    bool grounded = false;
    [HideInInspector] public Rigidbody rbody;
    public float walk_speed = 4.0f;
    public float run_speed = 8.0f; 
    public float turnSensitivity = 5.0f;

    void Start()
    {
        rbody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        locomotion();
        RotationByKeyboard();
    }


    void OnCollisionStay()
    {
        grounded = true;
    }

    void OnCollisionExit()
    {
        grounded = false;
    }
    float time = 0;
    void locomotion()
    {
        if (grounded)
        {
            float v = Input.GetAxisRaw("Vertical");
            float curAcc = run_speed;
            if (v != 0)
            {
                if (Input.GetKey(KeyCode.LeftShift)) { curAcc = walk_speed; }
                if (v < 0 || v == 0) { curAcc = 0; }//No walking backwards

                float V_velocityChange = Mathf.Lerp(rbody.velocity.z, v * curAcc, time);
                Vector3 targetVelocity = transform.TransformDirection(new Vector3(0, 0, V_velocityChange));
                rbody.velocity = targetVelocity;
                time += Time.deltaTime * curAcc;
            }
            else if (v == 0 && rbody.velocity.magnitude == 0) //when input and player velocity are both 0
            {
                time = 0;
                rbody.velocity = Vector3.zero;
            }
        }

    }
    float currentRotation = 0; //Stores current rotation value calculated by the functions below

    void RotationByKeyboard()
    {
        float h = Input.GetAxisRaw("Horizontal"); //left & right input from either keyboard or controller

        Quaternion newRotation;

        currentRotation += h * turnSensitivity;
        currentRotation = Mathf.Repeat(currentRotation, 360);
        newRotation = Quaternion.Euler(0, currentRotation, 0);
        transform.rotation = newRotation;
    }
    void RotationByXmouse()
    {
        float mouseX = Input.GetAxisRaw("Mouse X"); //mouse left & right movement

        Quaternion newRotation;

        currentRotation += mouseX * turnSensitivity;
        currentRotation = Mathf.Repeat(currentRotation, 360);
        newRotation = Quaternion.Euler(0, currentRotation, 0);
        transform.rotation = newRotation;
    }

}
