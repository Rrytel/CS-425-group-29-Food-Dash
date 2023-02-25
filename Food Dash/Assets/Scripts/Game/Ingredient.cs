using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*   add this script to an object and it will be considered an ingredient
*/
public class Ingredient : MonoBehaviour
{
	public IngredientTypes type  = IngredientTypes.Dough;

	string originalName;

	void Start ()
	{
		originalName = name;
		name = originalName + " (" + type.ToString () + ")";
	}

	/*
	*	attempt to add the ingredient to the food
	*	when entering the food trigger box
	*/
	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag ("plate"))
		{
			other.transform.parent.GetComponent <Plate> ().AddIngredient (this);
		}
	}
}

public enum IngredientTypes
{
	Dough,
	Sauce,
	Bun,
	Patty,
	Cheese,
	Lettuce,
	Potato,
	Salt,
	Beverage,
	Ice,
	Cup
}
