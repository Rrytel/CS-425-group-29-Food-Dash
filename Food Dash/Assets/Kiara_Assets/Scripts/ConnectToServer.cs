using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gives access to all multiplayer tools that Photon provides
//Callback = a fcn that gets automatically called by Photon when a certain event happens
using Photon.Pun;
using Photon.Realtime; //New
//To switch/load scenes
using UnityEngine.SceneManagement;
//MonoBehaviourPunCallbacks gives us access to different Pun fcn
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    //Connecting to Photon server
    private void Start()
    {
            //PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Trying to Connect to server...");
       
        //PhotonNetwork.AutomaticallySyncScene = true;
    }

    //Check when have successfully connected to our server
    //Anything in this fcn will get called when successfully connected to the server
    public override void OnConnectedToMaster()
    {
        
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby(); //power to create and join rooms if successfuly connected to server
        //PhotonNetwork.AutomaticallySyncScene = true;

    }

    //When have successfully joined the lobby, load Join Scene
    public override void OnJoinedLobby()
    {
        //Debug.Log("Joined the room");
        SceneManager.LoadScene("JoinScene");
        

        
    }


}
