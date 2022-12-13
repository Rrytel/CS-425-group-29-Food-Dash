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

    //Note: When you create a room, you're automatically joining that room
    //Calling it from the Create Button therefore needs to be public
    public void CreateRoom()
    {

        //if not connected, won't try to create a room
        if(!PhotonNetwork.IsConnected)
            return;

        //Creating max number of players allowed in a room
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 3;

        //Note: ADD where to make sure input field isn't empty before creating room
        //need string input for room name-> name of room will be whatever we write in the "createInput" input field
        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinRoom()
    {
        //Joining the room of whatever is in the joinINput text field
        PhotonNetwork.JoinRoom(joinInput.text);
    }
   
     //using callback fcn that's automatically called once joined a room
     public override void OnJoinedRoom()
    {
        //Transition to actual game scene
        PhotonNetwork.LoadLevel("food_dash");
    }
}
