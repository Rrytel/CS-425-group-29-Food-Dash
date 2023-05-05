using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoChopping : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip ChopTomato; // Renamed from chopSound


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tomato"))
        {
            audioSource.PlayOneShot(ChopTomato); // Updated from chopSound
        }
    }

}

