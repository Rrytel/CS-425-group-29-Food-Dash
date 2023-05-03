using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFood : MonoBehaviour
{
    GameObject direction;
    public GameObject food;
    public Transform IPoint;
    public AudioSource tomatoSound;

    void spawnFood()
    {
        Instantiate(food, IPoint.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player touching");
            spawnFood();
            tomatoSound.Play();
            StartCoroutine(StopTomatoSound());
        }
    }

    IEnumerator StopTomatoSound()
    {
        yield return new WaitForSeconds(3f); // wait for 3 seconds
        tomatoSound.Stop(); // stop the tomato sound after 3 seconds
    }
}

