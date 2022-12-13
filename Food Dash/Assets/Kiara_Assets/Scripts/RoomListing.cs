using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    //setting room name text
    [SerializeField]
    private Text _text;

    public RoomInfo RoomInfo { get; private set; }

    //accepting a RoomInfo class so can just pull from it
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        //setting the public room info accessor defined above to the passed in parameter
        RoomInfo = roomInfo;

        _text.text = roomInfo.MaxPlayers + ", " + roomInfo.Name;
    }

}
