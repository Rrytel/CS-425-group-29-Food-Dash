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
		if (Input.GetKeyDown (KeyCode.P))
		{
			foodPrefab.GetComponent <Food> ().type = FoodTypes.Pizza;

			Instantiate (foodPrefab);
		}
		else if (Input.GetKeyDown (KeyCode.B))
		{
			foodPrefab.GetComponent<Food>().type = FoodTypes.Burger;

			Instantiate (foodPrefab);
		}
		else if (Input.GetKeyDown (KeyCode.F))
		{
			foodPrefab.GetComponent <Food> ().type = FoodTypes.Fries;

			Instantiate (foodPrefab);
		}
		else if (Input.GetKeyDown (KeyCode.D))
		{
			foodPrefab.GetComponent <Food> ().type = FoodTypes.Drink;

			Instantiate (foodPrefab);
		}
	}
}
