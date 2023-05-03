using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatBin : MonoBehaviour
{
    public AudioClip ratSound;  // audio clip for rat sound

    private AudioSource audioSource; // audio source component

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // get audio source component
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        //check if the object colliding with it is a rat
        if (collision.gameObject.tag == "Rat")
        {
            Debug.Log("A rat is touching the bin");
            RatTouching(collision);
        }
        else
        {
            Debug.Log("It's not a rat touching the bin");
        }
    }

    //destroy the passed in rat object and play rat sound
    void RatTouching(Collision rat)
    {
        audioSource.PlayOneShot(ratSound); // play rat sound
        Destroy(rat.gameObject); // destroy rat object
    }
}


