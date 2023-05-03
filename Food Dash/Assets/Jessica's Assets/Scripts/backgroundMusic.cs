using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip gameOn; // The background music to play
    public float musicVolume = 0.1f; // The volume of the background music
    public float inactivityThreshold = 20f; // The amount of time (in seconds) to wait before stopping the music

    private float lastActivityTime; // The time when the user last used the controller
    private bool isPlaying; // Whether or not the background music is currently playing
    private AudioSource audioSource; // The audio source component to play the background music

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = gameOn;
        audioSource.volume = musicVolume;
        audioSource.loop = true;
        lastActivityTime = Time.time;
    }

    void Update()
    {
        // Check if the user is currently using the controller
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            lastActivityTime = Time.time;
            if (!isPlaying)
            {
                isPlaying = true;
                audioSource.Play();
            }
        }
        else if (Time.time - lastActivityTime > inactivityThreshold && isPlaying)
        {
            isPlaying = false;
            audioSource.Stop();
        }
    }
}
