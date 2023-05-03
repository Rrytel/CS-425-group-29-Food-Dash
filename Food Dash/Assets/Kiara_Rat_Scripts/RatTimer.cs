using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatTimer : MonoBehaviour
{
    public GameObject ratObject;
    float spawnTime;
    float gameSpawn;
    public List<GameObject> spawns;
    public GameObject gameController;

    //delay for timer to start for rats to start spawning
    private float delay = 45.0f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {

        spawnTime = 15;


        //Based on the round the user is in, spawn a diff. # of rats
        int day = gameController.GetComponent<Round>().GetDay ();
        if (day == 0 || day == 1)
        {
            gameSpawn = 3;
        }else if(day == 2){
            gameSpawn = 5;
        }else if (day == 3)
        {
            gameSpawn = 7;
        } else {
            gameSpawn = day + 4;
        }
        //gameSpawn = 5;
    }

    // Update is called once per frame
    //Every frame it will subtract the duration of the previous frame(delta time) from am.
    void Update()
    {

        //rats won't spawn until 45 sec has passed in the game and then every 15 sec for ever many rats are to spawn that day
        timer += Time.deltaTime;
        if(timer > delay)
        {
                spawnTime = spawnTime - Time.deltaTime;
            if (spawnTime <= 0)
            {
                Debug.Log("This is the spawn time: " + spawnTime);
                Debug.Log("Time has passed game playing: " + Time.time);
                //reset the time to 15 sec
                spawnTime = 15f;

                int index = Random.Range(0, spawns.Count);
                Debug.Log($"index: {index}");
                //give the rat a random pos.
                var randPos = spawns[index].transform.position;

                //create an object at the random positon calculated
                if(gameSpawn > 0)
                {

                Instantiate(ratObject, randPos, Quaternion.identity);

                    gameSpawn--;
                }
                //Debug.Log(spawnTime);
                Debug.Log("List Size: " + spawns.Count.ToString());

                for (int i = 0; i < spawns.Count; i++)
                {
                    Debug.Log(spawns[i]);

                }
                //for (int j=0)
                //spawnTime = 45f;

            }
            if (spawnTime == 0)
            {
                Debug.Log("Timer has run out!");
            }
        }


        //Debug.Log("Time has passed game playing: " + Time.time);
    }
}
