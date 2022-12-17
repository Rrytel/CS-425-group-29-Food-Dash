using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

//Credit: https://www.youtube.com/watch?v=KGzMxalSqQE
public class RoomListingsMenu : MonoBehaviourPunCallbacks
{
    //NOTE: ROOMINFO =

    //Note: SerializeField = Its main purpose is to save the state of an object in order to be able to recreate it when needed.
    
    [SerializeField]
    private RoomListing _roomListingPrefab;

    //where the room listings are added to
    [SerializeField]
    private Transform _content;


    //storing references of list of rooms that have been added
    private List<RoomListing> _listings = new List<RoomListing> ();

    //Get information about created room
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //populate "Content" based on the room info
        //go thr. every room listing
        foreach (RoomInfo room in roomList)
        {
            //if the room is being removed from the list, it will be set to true
            if(room.RemovedFromList)
            {
                //can search through listing to see which room to destroy
                //Comparing room names since they're unique
                //Gets the index of whichever listing has the same room name as which we received
                int idx = _listings.FindIndex(x => x.RoomInfo.Name == room.Name);

                //if the index is found, destroy GameObject for the listing
                //AND destroy listing from the list as well
                if(idx != -1)
                {
                    Destroy(_listings[idx].gameObject);
                    _listings.RemoveAt(idx);
                }
            }
            //Added rooms to the list
            else
            {
                RoomListing listing = Instantiate(_roomListingPrefab, _content);

                //if the listing doesn't equal null, pass in room info to update text field in RoomListing
                if (listing != null)
                {
                    listing.SetRoomInfo(room);
                    _listings.Add(listing);
                }
            }

            
        }
    }
}
