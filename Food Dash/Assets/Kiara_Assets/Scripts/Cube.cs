using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//Place holder script until combine vr xr implementation on player object//
public class Cube : MonoBehaviourPunCallbacks
{
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //to move the cube with the keys
        //transform is a vec3 = x,y,z and how much we're moving it in each of those dirs.
        //transform.Translate(1f * Time.deltaTime, 0f, 0f);
        if(photonView.IsMine)
        {
            //transform.Translate(moveSpeed * Input.GetAxis("Horizontal"), 1.0f, moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime);

            //source: https://www.c-sharpcorner.com/article/transforming-objects-movement-using-c-sharp-scripts-in-unity/#:~:text=Move%20and%20Rotate%20the%20object%20by%20Arrow%20key%20press&text=Press%20%22Left%20%26%20Right%20Arrow%22,will%20move%20forward%20and%20backwards.
            
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.transform.Translate(Vector3.forward * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.Translate(Vector3.back * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.Translate(Vector3.left* Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(Vector3.right * Time.deltaTime);
            }

        }
   
        
    }
}
