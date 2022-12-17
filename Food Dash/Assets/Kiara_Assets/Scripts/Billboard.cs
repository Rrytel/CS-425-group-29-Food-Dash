using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credit: https://www.youtube.com/watch?v=v75whPrOelU
public class Billboard : MonoBehaviour
{
    //Want Username to face the camera and be flat against the screen

    //Store reference to the camera
    Camera cam;

    void Update()
    {
        //if don't have a camera, will find one in the scene
        if(cam == null)
        {
            cam = FindObjectOfType<Camera>();
        }

        //if still didn't find a camera, just return
        if (cam == null)
            return;

        //face the camera
        transform.LookAt(cam.transform);

        //flip username along the vertical axis
        transform.Rotate(Vector3.up * 180);
    }
}
