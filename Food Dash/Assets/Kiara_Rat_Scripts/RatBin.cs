using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        //check if the object colliding with it is a rat
        if(collision.gameObject.tag == "Rat")
        {
            Debug.Log("A rat is touching the bin");
            RatTouching(collision);
        }
        else
        {
            Debug.Log("It's not a rat touching the bin");
        }
    }

    //destroy the passed in rat object
    void RatTouching(Collision rat)
    {
        Destroy(rat.gameObject);
    }
}


