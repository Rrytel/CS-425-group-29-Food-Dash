using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ratSpawner_UnitTest : MonoBehaviour
{
    // Start is called before the first frame update

    /* Tests:
     * - check that it spawns every determined amount of seconds
     */

    float spawnTime;
    float timePassed;
    public GameObject rat;
    public List<GameObject> spawns;
    bool inSceneTest;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if timer has run out, meaning 25 seconds have passed
        
           //check amount of time passed
            timePassed = Time.time;
          

        //Every frame it will subtract the duration of the previous frame(delta time) from am.
        spawnTime = spawnTime - Time.deltaTime;
        
        if (spawnTime < 0)
            {
                //reseting the time to spawn rats to be every 25 seconds within the game scene
                spawnTime = 25f;
              
                //index of spawn object in list
                 int index = 1;
                

                //give the rat a random pos.
                var randPos = spawns[index].transform.position;


            //if the amount of time passed is evenly divisible by 25, means that timePassed is a multiple of 25
                    if (timePassed % 25 == 0)
                    {
       
                       

                            Instantiate(rat, randPos, Quaternion.identity);
                            //check if game object in scene
                            inSceneTest = checkInScene();
                        


                    }

            if (inSceneTest)
            {
                Debug.Log("Unit test passed!");
            }
            else
            {
                Debug.Log("Unit test failed :(");
            }
        }
                

 


    }

    //pass in a list object check if item is apart of public list of spawn holes


    bool checkInScene()
    {
        //Check if there are any objects within the scene with the tag "ratTest"
        GameObject[] ratObjects;
        ratObjects = GameObject.FindGameObjectsWithTag("ratTest");

        if (ratObjects.Length == 0)
        {
            Debug.Log("Test Failed, no objects with 'rat' in scecne");
            return false;
        }
        else
        {
            Debug.Log("Test passed! There's a rat in the scene");
            return true;
        }
    }
}
