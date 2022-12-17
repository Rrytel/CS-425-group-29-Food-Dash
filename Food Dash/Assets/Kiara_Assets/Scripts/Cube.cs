using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
//Credit: https://www.youtube.com/watch?v=mw9HvP0hbLc&t=360s
public class Cube : MonoBehaviourPunCallbacks
{
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = .4f;
    }

    // Update is called once per frame
    void Update()
    {
        //to move the cube with the keys
        //transform is a vec3 = x,y,z and how much we're moving it in each of those dirs.
        //transform.Translate(1f * Time.deltaTime, 0f, 0f);
        if(photonView.IsMine)
        {
            transform.Translate(moveSpeed * Input.GetAxis("Horizontal"), 0.0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);
        }
        
    }
}
