using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*   add this script to an object and it will be considered an ingredient
*/
public class Ingredient : MonoBehaviour
{
	public IngredientTypes type  = IngredientTypes.Bun;

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
		test_food_script foodScript = GetComponent <test_food_script> ();

		if (other.CompareTag ("plate") && foodScript.cooked && !foodScript.burnt)
		{
			other.transform.parent.GetComponent <Plate> ().AddIngredient (this);
		}
	}
}

public enum IngredientTypes
{
	Bun,
	Patty,
	Cheese,
	Lettuce,
	Tomato
}
