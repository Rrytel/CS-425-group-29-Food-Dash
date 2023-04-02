using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_trigger_interaction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        switch(other.name)
        {
            case "OpenN":
                other.GetComponentInParent<door_way_scr>().OpenFromNorth();
                break;

        }
        switch (other.name)
        {
            case "OpenS":
                other.GetComponentInParent<door_way_scr>().OpenFromSouth();
                break;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        switch(other.name)
        {
            case "StayOpen":
                other.GetComponentInParent<door_way_scr>().AddEntity();
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        switch (other.name)
        {
            case "StayOpen":
                other.GetComponentInParent<door_way_scr>().RemoveEntity();
                break;
        }
    }
}
