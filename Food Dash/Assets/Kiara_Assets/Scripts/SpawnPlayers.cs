using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
//Credit: https://www.youtube.com/watch?v=93SkbMpWCGo
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
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), -14);
        //Will spawn player on the server so each player is visible within the game
        //Quaternion.identity = rotation
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }

 
}
