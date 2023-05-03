using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoChopping : MonoBehaviour
{
    public AudioClip ChopTomato;
    private AudioSource audioSource;
    private bool isChopping = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tomato") && !isChopping)
        {
            isChopping = true;
            audioSource.PlayOneShot(ChopTomato);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tomato"))
        {
            isChopping = false;
        }
    }
}
