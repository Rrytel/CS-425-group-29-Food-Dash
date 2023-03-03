using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;


public class SpawnPlayers : MonoBehaviour
{

    public GameObject playerPrefab; //will store player prefab
    //Boundaries for where can randomly spawn player- aka fields to enter in inspector
    float minX;
    float maxX;
    float minY;
    float maxY;

    // Start is called before the first frame update
    private void Start()
    {
        //sPhotonNetwork.AutomaticallySyncScene = true;

        Scene currentScene = SceneManager.GetActiveScene();
        string scene = currentScene.name;

        //Vector2 randomPosition;

        if(scene == "WaitingForPlayer")
        {
            minX = -3.68f;
            maxX = 4.03f;
            minY = 0.74f;
            maxY = 0.91f;
        }
        else if(scene == "food_dash_Kiara")
        {
            minX = 8.42f;
            maxX = 6.53f;
            minY = 0.487f;
            maxY = 0.487f;

            //randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        //Will spawn player on the server so each player is visible within the game
        //Quaternion.identity = rotation
        //if(PhotonNetwork.CountOfPlayers == 2)
        //{
            PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        //}
        //else
        //{
            //Debug.Log("Both players aren't in yet");
        //}
        //PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);


    }

 
}
