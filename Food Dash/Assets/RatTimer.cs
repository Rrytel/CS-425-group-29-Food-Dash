using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatTimer : MonoBehaviour
{
    public GameObject ratObject;
    public float spawnTime = 45;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //Every frame it will subtract the duration of the previous frame(delta time) from am.
    void Update()
    {
        spawnTime = spawnTime - Time.deltaTime;
        if (spawnTime < 0)
        {
            //reset the time to 45 sec
            spawnTime = 45f;

            //give the rat a random pos.
            var randPos = new Vector3(Random.Range(-4.96f, 0.04f), 0.19f, Random.Range(-3.18f, -0.7f));
            //create an object at the random positon calculated
            Instantiate (ratObject, randPos, Quaternion.identity);
            Debug.Log(spawnTime);

            //spawnTime = 45f;
            
        } else if (spawnTime == 0)
        {
            Debug.Log("Timer has run out!");
        }
    }
}
