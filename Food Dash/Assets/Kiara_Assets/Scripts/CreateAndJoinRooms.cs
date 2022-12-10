using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//allows to create inputfield vars. and anything else involving UI
using UnityEngine.UI;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
    public InputField joinInput;

    //Note: When you create a room, you're automatically joining that room
    //Calling it from the Create Button therefore needs to be public
    public void CreateRoom()
    {
        //need string input for room name-> name of room will be whatever we write in the "createInput" input field
        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }
   
     //using callback fcn that's automatically called once joined a room
     public override void OnJoinedRoom()
    {
        //Transition to actual game scene
        PhotonNetwork.LoadLevel("GameScene");
    }
}
