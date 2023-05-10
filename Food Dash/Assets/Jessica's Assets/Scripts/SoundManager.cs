using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource backgroundMusic;
    public AudioClip cookingSound;
    public AudioClip choppingSound;
    public AudioClip sizzlingSound;
    public AudioClip successSound;
    public AudioClip failureSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayBackgroundMusic()
    {
        if (!backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }

    public void PlayCookingSound()
    {
        PlaySound(cookingSound);
    }

    public void PlayChoppingSound()
    {
        PlaySound(choppingSound);
    }

    public void PlaySizzlingSound()
    {
        PlaySound(sizzlingSound);
    }

    public void PlaySuccessSound()
    {
        PlaySound(successSound);
    }

    public void PlayFailureSound()
    {
        PlaySound(failureSound);
    }

    private void PlaySound(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }
}
