using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class FirstPersonController : MonoBehaviour
{
    //comps
    public Rigidbody RB;
    //floats
    public float MouseSensitivity = 3;
    public float WalkSpeed = 10;
    //objs
    public Camera Eyes;
    
    void Start()
    {
        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        //get mouseaxi
        float xRot = Input.GetAxis("Mouse X") * MouseSensitivity;
        float yRot = -Input.GetAxis("Mouse Y") * MouseSensitivity;
        //horrot
        transform.Rotate(0,xRot,0);
        //get rot
        Vector3 Prot = Eyes.transform.localRotation.eulerAngles;
        //add changetorot
        Prot.x += yRot;
        //if's
        if (Prot.x < -180)
        {
            Prot.x += 360;
        }
        if (Prot.x > 180)
        {
            Prot.x -= 360;
        }
        //clamp minmax
        Prot = new Vector3(Mathf.Clamp(Prot.x, -65, 40), 0, 0);
        //plug back in
        Eyes.transform.localRotation = Quaternion.Euler(Prot);


        if (WalkSpeed > 0)
        {
            //set 0
            Vector3 move = Vector3.zero;
            //fore
            if (Input.GetKey(KeyCode.W))
                move += transform.forward;
            //aft
            if (Input.GetKey(KeyCode.S))
                move -= transform.forward;
            //left
            if (Input.GetKey(KeyCode.A))
                move -= transform.right;
            //right
            if (Input.GetKey(KeyCode.D))
                move += transform.right;
            //setspeed
            move = move.normalized * WalkSpeed;
            //plug back in
            RB.velocity = move;
        }
    }

}


