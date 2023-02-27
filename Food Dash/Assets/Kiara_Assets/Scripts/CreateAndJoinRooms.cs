using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//allows to create inputfield vars. and anything else involving UI
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;
    //checks if all players are connected before syncing them up and sending them to the created room
    bool allConnected = false;

    //Have the timer run only on the host, ev. time when timer lapses send out a network message to all game clients, that notifies it and then - ASK RYAN about his timer script - have to put photon into his script
    //RPC's - PHOTON
    //Have the server be the authority for the shared state

    //Note: When you create a room, you're automatically joining that room
    //Calling it from the Create Button therefore needs to be public
    public void CreateRoom()
    {

        //if not connected, won't try to create a room
        if(!PhotonNetwork.IsConnected)
            return;

        //Creating max number of players allowed in a room
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;

        

        //Note: ADD where to make sure input field isn't empty before creating room
        //need string input for room name-> name of room will be whatever we write in the "createInput" input field
        PhotonNetwork.CreateRoom(createInput.text, roomOptions); //automatically calls OnJoinedRoom
    }
    //if client has successfully connected to the room
    public void JoinRoom()
    {
        //Joining the room of whatever is in the joinINput text field
        PhotonNetwork.JoinRoom(joinInput.text);
    }
   
     //using callback fcn that's automatically called once joined a room
     public override void OnJoinedRoom()
    {
        //send the master to waiting for others scene - then check how many players are in the scene- if 2, then load the game scene with both of them in it
        PhotonNetwork.LoadLevel("WaitingForPlayer");
        allConnected = checkIfAllPlayers();
        //Transition to actual game scene
        if (allConnected)
        {
            PhotonNetwork.LoadLevel("food_dash_Kiara");
        }
        else
        {
            Debug.Log("Sorry not enough players");
        }
       
    }

    bool checkIfAllPlayers()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            Debug.Log("All players are in the room!");
        }
        else
        {
            Debug.Log("Not enough players in the room: " + PhotonNetwork.CurrentRoom.PlayerCount);
        }
        return false;

    }
}
