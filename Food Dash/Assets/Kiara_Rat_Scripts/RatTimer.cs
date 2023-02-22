using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatTimer : MonoBehaviour
{
    public GameObject ratObject;
    public float spawnTime = 15;
    public List<GameObject> spawns;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = 1;
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

            int index = Random.Range(0, spawns.Count);
            Debug.Log("index: " + index);
            //give the rat a random pos.
            var randPos = spawns[index].transform.position;
            //var randPos = new Vector3(Random.Range(-4.96f, 0.04f), 0.19f, Random.Range(-3.18f, -0.7f));
            //create an object at the random positon calculated
            Instantiate(ratObject, randPos, Quaternion.identity);
            //Debug.Log(spawnTime);
            Debug.Log("List Size: " + spawns.Count.ToString());

            for (int i = 0; i < spawns.Count; i++)
            {
                Debug.Log(spawns[i]);
            }

            //spawnTime = 45f;

        }
        if (spawnTime == 0)
        {
            Debug.Log("Timer has run out!");
        }

        Debug.Log("Time has passed game playing: " + Time.time);
    }
}



