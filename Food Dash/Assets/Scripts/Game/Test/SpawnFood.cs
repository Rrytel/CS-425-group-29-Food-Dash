using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*	for testing food spawning
*	and serving customers
*/
public class SpawnFood : MonoBehaviour
{
	public GameObject foodPrefab;

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetKeyDown (KeyCode.B))
		{
			foodPrefab.GetComponent<Food>().type = FoodTypes.Burger;

			Instantiate (foodPrefab);
		}
		else if (Input.GetKeyDown (KeyCode.C))
		{
			foodPrefab.GetComponent<Food> ().type = FoodTypes.Cheeseburger;

			Instantiate (foodPrefab);
		}
		else if (Input.GetKeyDown (KeyCode.D))
		{
			foodPrefab.GetComponent <Food> ().type = FoodTypes.Drink;

			Instantiate (foodPrefab);
		}
	}
}
