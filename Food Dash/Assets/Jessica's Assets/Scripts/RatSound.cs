using UnityEngine;

public class RatSound : MonoBehaviour
{

    public AudioClip ratSound;
    private bool isTouchingRat = false;

    void Update()
    {
        if (isTouchingRat)
        {
            PlayRatSound();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Rat")
        {
            isTouchingRat = true;
            Invoke("StopRatSound", 3f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Rat")
        {
            isTouchingRat = false;
            StopRatSound();
        }
    }

    void PlayRatSound()
    {
        if (ratSound != null)
        {
            AudioSource.PlayClipAtPoint(ratSound, transform.position);
        }
    }

    void StopRatSound()
    {
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in allAudioSources)
        {
            if (audioSource.clip == ratSound)
            {
                audioSource.Stop();
            }
        }
    }
}
