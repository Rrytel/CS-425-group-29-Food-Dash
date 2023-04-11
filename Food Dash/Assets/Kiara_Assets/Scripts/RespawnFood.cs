using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnFood : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject direction;
    public GameObject food;
    public Transform IPoint;
    //private void OnTriggerEnter(Collider other)
    /*{
        OnMouseOver();

    }*/

    //check if text is active and if the user presses A, and if both true then spawn a food item
    void spawnFood()
    {
        //if the text direction is active, and the user presses A then spawn food
        //if (direction.activeSelf == true && Input.GetKeyDown(KeyCode.A))
        //{
        //create a copy of the desired food item that's passed in
        //Instantiate(food, new Vector3 (4.32f, 1.179f, -8.41f), Quaternion.identity);
        //}
        Instantiate(food, IPoint.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if the object colliding with it is a rat
        /*if (collision.gameObject.tag == "Player")
        {
            Debug.Log("A player is touching the crate");
            spawnFood();
        }
        else
        {
            Debug.Log("It's not a player touching the crate");
        }*/
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player touching collision");
            
        }
        //if ((transform.position - collision.position))
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player touching");
            //Instantiate(food, IPoint.position, Quaternion.identity);
            spawnFood();
        }
    }

   /* private void Update()
    {
        float distance = 100;
        if((transform.position - GameObject.FindGameObjectWithTag("Player").transform.position ).sqrMagnitude > distance * distance)
        {
            spawnFood();
        }
    }*/

    /* void OnMouseOver()
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
    */
}
