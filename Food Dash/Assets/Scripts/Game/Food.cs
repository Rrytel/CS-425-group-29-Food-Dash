using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public FoodTypes type = FoodTypes.Pizza;

	string originalName;

	// stores the ingredients needed to prepare the food
	List <IngredientTypes> ingredients = new ();

	// Start is called before the first frame update
	void Start ()
	{
		originalName = name;

		name = originalName + " (" + type.ToString() + ")";
	}

	/*
	*	attempt to add the food to the order
	*	when entering the customer trigger box
	*/
	void OnTriggerEnter (Collider other)
	{
		// might want to change this to "Plate" in the future
		if (other.CompareTag ("Customer"))
		{
			other.gameObject.GetComponent <Order> ().AddFood (this);
		}
	}
}

public enum FoodTypes
{
	Pizza,
	Burger,
	Fries,
	Drink
}
