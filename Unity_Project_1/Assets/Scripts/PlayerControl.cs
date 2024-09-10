using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class NewBehaviourScript : MonoBehaviour
{
    private Rigidbody Player;
    private Camera playercam;

    Vector2 camrotation;

    [Header("player settings")]
    public float speed = 10.0f;
    public float jumpheight = 5.0f;
    public float GroundDetectDistance = 1;
    public float sprintmultiplier = 2.5f;


    [Header("player settings")]
        public bool sprintToggleOption = false;
    public float xcamsensitivity = 2.0f;
    public float ycansensitivity = 2.0f;
    public float camRotioatonLimit = 90f;
    public float mouseSensitivity = 2.0F;
    public bool sprintmode = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponent<Rigidbody>();
        playercam = transform.GetChild(0).GetComponent<Camera>();

        camrotation = Vector2.zero;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;


    }

    // Update is called once per frame
    void Update()
    {
        camrotation.x += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        camrotation.y += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        

        camrotation.y = Mathf.Clamp(camrotation.y, -camRotioatonLimit, camRotioatonLimit);

        playercam.transform.localRotation = Quaternion.AngleAxis(camrotation.y, Vector3.left);
        playercam.transform.localRotation = Quaternion.AngleAxis(camrotation.x, Vector3.up);

        

        Vector3 temp = Player.velocity;

        if(sprintToggleOption)
        {
            if(Input.GetKey(KeyCode.LeftShift))
                sprintmode = true;

            if (Input.GetKeyUp(KeyCode.LeftShift))
                sprintmode = false;
        }

        if(sprintToggleOption)
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxisRaw("Horizontal") <= 0)
                sprintmode = true;

            if (Input.GetAxisRaw("Vertical") <= 0)
                sprintmode = false;
        }
        
        if (Input.GetKey(KeyCode.LeftShift))
           sprintmode = true;


        if (sprintmode)
            temp.x = Input.GetAxisRaw("Vertical") * speed * sprintmultiplier;
         


        if (!sprintmode)
            temp.z = Input.GetAxisRaw("Horizontal") * speed;
        

       if(Input.GetKeyUp(KeyCode.LeftShift))
            sprintmode = false;




        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -transform.up, GroundDetectDistance))
            temp.y = jumpheight;


        Player.velocity = (temp.x * transform.forward) + (temp.z * transform.right) + (temp.y * transform.up);

    }
}
