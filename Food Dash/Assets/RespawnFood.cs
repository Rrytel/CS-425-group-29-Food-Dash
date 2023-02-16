using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFood : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject direction;
    public GameObject food;
    //private void OnTriggerEnter(Collider other)
    /*{
        OnMouseOver();

    }*/

    //check if text is active and if the user presses A, and if both true then spawn a food item
    void spawnFood()
    {
        //if the text direction is active, and the user presses A then spawn food
        if (direction.activeSelf == true && Input.GetKeyDown(KeyCode.A))
        {
            //create a copy of the desired food item that's passed in
            Instantiate(food, transform);
        }
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over Crate.");
        direction.SetActive(true); //makes the direction text visible
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse is not over Crate");
        direction.SetActive(false);
    }
}
