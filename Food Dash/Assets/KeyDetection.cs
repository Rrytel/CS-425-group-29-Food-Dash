using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
public class KeyDetection : MonoBehaviour
{
    private TextMeshPro playerTextOutput;
    // Start is called before the first frame update
    void Start()
    {
        
        //get component of output
        playerTextOutput = GameObject.FindGameObjectWithTag("PlayerTextOutput").GetComponentInChildren<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //where detect key, when hit key get TMP component from whichever object hit
    private void OnTriggerEnter(Collider other)
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        var key = other.GetComponentInChildren<TextMeshPro>();

        //if key doesn't have text mesh pro, then it's not a key - don't do anything
        if (key != null)
        {
            //get key feedback - from script component whenever hit the key
            var keyFeedback = other.gameObject.GetComponent<KeyFeedback>();
            

            //get key feedback from the key
            if (other.gameObject.GetComponent<KeyFeedback>().canHitKeyAgain == true)
            {
                //check what the TMP text is on the key
                if (key.text == "SPACE")
                {
                    //add a space to the text output
                    playerTextOutput.text += " ";
                }
                else if (key.text == "<-")
                {
                    //remove last letter from the string by using substring from first index to last minus 1
                    playerTextOutput.text = playerTextOutput.text.Substring(0, playerTextOutput.text.Length - 1);
                }else if(key.text == "CREATE")
                {
                    if(playerTextOutput.text != null)
                    {
                        CreateRoom(playerTextOutput.text);
                    }
                    
                    //PhotonNetwork.CreateRoom(key.text, roomOptions);
                }
                else
                {
                    //if none of the above are true, want to add whatever is on the key to the output displayed
                    playerTextOutput.text += key.text;
                }
                keyFeedback.isKeyHit = true;

            }
        }
    }
    public void CreateRoom(string roomName)
    {

        //if not connected, won't try to create a room
        if (!PhotonNetwork.IsConnected)
            //Debug.Log("User isn't connected");
            return;

        //Creating max number of players allowed in a room
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        //string roomState = (string)(PhotonNetwork.room.customProperties["Kills"];



        //Note: ADD where to make sure input field isn't empty before creating room
        //need string input for room name-> name of room will be whatever we write in the "createInput" input field
        PhotonNetwork.CreateRoom(roomName, roomOptions); //automatically calls OnJoinedRoom
        //roomNames.Add("progress");

    }

    public void JoinRoom(string roomName)
    {
       
            PhotonNetwork.JoinRoom(roomName);
        
       
        //Joining the room of whatever is in the joinINput text field

    }
    public void OnJoinedRoom()
    {

        //send the master to waiting for others scene - then check how many players are in the scene- if 2, then load the game scene with both of them in it
        PhotonNetwork.LoadLevel("food_dash");
    }
}
