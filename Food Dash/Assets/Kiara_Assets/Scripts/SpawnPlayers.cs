using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{

    public GameObject playerPrefab; //will store player prefab
    //Boundaries for where can randomly spawn player- aka fields to enter in inspector
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Start is called before the first frame update
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
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
