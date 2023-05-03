using UnityEngine;

public class ChopLettuceSound : MonoBehaviour
{

    public AudioClip chopLettuce;

    private bool isChopping = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Lettuce")
        {
            isChopping = true;
            PlayChopLettuceSound();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Lettuce")
        {
            isChopping = false;
        }
    }

    void PlayChopLettuceSound()
    {
        if (chopLettuce != null && isChopping)
        {
            AudioSource.PlayClipAtPoint(chopLettuce, transform.position);
        }
    }
}
