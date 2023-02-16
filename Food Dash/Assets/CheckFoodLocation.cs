using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckFoodLocation : MonoBehaviour
{
    //An event that checks whether the food object is touching another game object with the tag 
    //"plate" and if so, destroy the plate- checks first if this food item is listed in the text box

    public GameObject foodItem; //creating a game object and whichever object is put into this variable int
                                //inspector is the referenced foodItem
    void OnCollisionEnter(Collision collision)
    {
        //public GameObject foodItem;

        if (collision.gameObject.CompareTag("plate"))
        {
            Debug.Log("It's touching the plate");
            Destroy(collision.gameObject); //destroying the plate if touching it
             //Instantiate(foodItem);
            
           

            //Note: Object.Instatiate = returns an object which is a clone of the original obj.

        }
    }
}


