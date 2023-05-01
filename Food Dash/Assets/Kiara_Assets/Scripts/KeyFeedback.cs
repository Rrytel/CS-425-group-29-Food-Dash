using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFeedback : MonoBehaviour
{
    public bool isKeyHit = false;
    public bool canHitKeyAgain = false;

    //original y position of key
    private float originalYPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalYPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //if the key has been hit, reset that it's been hit and make it where u can't hit the key again, while also 
        //moving the key down briefly to simulate a key being pressed down
        if(isKeyHit)
        {
            isKeyHit = false;
            canHitKeyAgain = false;
            //moving down, by adding a neg. value to transform
            transform.position += new Vector3(0, -0.03f, 0);
        }

        //if the current y pos is less than original position, move the key back up
        if(transform.position.y < originalYPosition)
        {
            //move back up by adding a pos. val. to transform
            transform.position += new Vector3(0, 0.005f, 0);
        }
        else
        {
            //if not, then it is back to the original position and therefore can be hit again
            canHitKeyAgain=true;
        }


    }
}
