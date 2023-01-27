using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rigidbody_Controller : MonoBehaviour
{


    enum animState { idle ,locomotion,hurt,dead}; //Animation States

    public enum rotType { wasd, mousePos, screenPos}; //Rotation Type

    public rotType currRotType;

    [SerializeField] float walk_speed = 4.0f;
    [SerializeField] float run_speed = 8.0f; 

    [SerializeField] float turnSensitivity = 5.0f;

    bool grounded = false;
    public bool canMove = true;
    [HideInInspector] public Rigidbody rbody;
    private Animator anim;

    public GameObject activeCamera; // required by for the RotationByCursor function
    public Camera pointerCamera;  // required by for the RotationByCursor function
    public bool hideCursor = true;
    // Start is called before the first frame update
    void Start()
    {
   
        if (currRotType == rotType.screenPos) {
            activeCamera = GameObject.FindGameObjectWithTag("ActiveCamera");
            pointerCamera = activeCamera.transform.GetChild(0).GetComponent<Camera>();
        }
        // The items above are necessary for the RotationByCursor function

        rbody = GetComponent<Rigidbody>();
        anim = this.gameObject.GetComponent<Animator>(); //Where is your animator? //gameObject.transform.GetChild(0)
        Cursor.visible = !hideCursor;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) {
            locomotion();
            RotationTypeHandler(currRotType);

        }
        aniamator(animStateHandler());
    }


    void OnCollisionStay()
    {
        grounded = true;
    }

    void OnCollisionExit()
    {
        grounded = false;
    }


    animState animStateHandler() { //What else determines the other animation states?
        if (rbody.velocity.x != 0 || rbody.velocity.z != 0)
        {
            return animState.locomotion;
        }
        else {
            return animState.idle;
        }
    }


    void aniamator(animState aState) {
       // Debug.Log("aniamator: " + aState);  //rigidbody.velocity

        switch (aState)
        {
            case animState.idle:
                anim.SetFloat("VelZ", 0);

                break;
            case animState.locomotion:

                float fwdSpeed = Vector3.Dot(rbody.velocity, transform.forward);
                anim.SetFloat("VelZ", fwdSpeed/ run_speed);

                break;
            case animState.hurt: 
                //Events when the player is hurt
            break;
            case animState.dead:
                //Events when the player is dead
            break;

        }
        
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
                        if (Input.GetKey(KeyCode.LeftShift)) {curAcc = walk_speed; }   
                        if (v < 0 || v == 0 ) { curAcc = 0; }//No walking backwards

                        float V_velocityChange = Mathf.Lerp(rbody.velocity.z, v * curAcc, time);
                        Vector3 targetVelocity = transform.TransformDirection(new Vector3(0, 0, V_velocityChange));
                        rbody.velocity = targetVelocity;
                        time += Time.deltaTime * curAcc; 
            }
            else if (v == 0 && rbody.velocity.magnitude==0) //when input and player velocity are both 0
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
    //Function: RotationByKeyboard
    // Rotates the player along the y-axis according the default horizontal axis input
    // Default horizontal axis can be accessed via Edit> Project Settings > Input Manager

    void RotationByXmouse()
    {
        float mouseX = Input.GetAxisRaw("Mouse X"); //mouse left & right movement
        
        Quaternion newRotation;

        currentRotation += mouseX * turnSensitivity;
        currentRotation = Mathf.Repeat(currentRotation, 360);
        newRotation = Quaternion.Euler(0, currentRotation, 0);
        transform.rotation = newRotation;
    }
    //Function: RotationByXmouse
    // Rotates the player along the y-axis according the left to right motion of the mouse

    Vector3 relativePosition;
    void RotationByCursor() {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 lookTarget = pointerCamera.ScreenToWorldPoint(mousePosition);
         relativePosition = lookTarget - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePosition, Vector3.up);
        transform.rotation = new Quaternion(0, rotation.y, 0, rotation.w);
    }
    //Function: RotationByCursor
    //Rotates the player to face the cursor according to where the cursor position intersects the 3D level
    //Recuires the use of a hidden camera within the scene cameras
    //The hidden camera must be in orthographic mode with dynamic resolution enabled

    void RotationTypeHandler(rotType curr) {

        switch (curr) {
            case rotType.wasd:
                RotationByKeyboard();
            break;
            case rotType.mousePos:
                RotationByXmouse();
            break;
            case rotType.screenPos:
                RotationByCursor();
            break;
        }
    }
    //Function: RotationTypeHandler
    //Chooses players rotation method according to the currRotType variable 

}
