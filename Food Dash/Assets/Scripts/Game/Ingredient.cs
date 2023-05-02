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

		if (other.CompareTag ("plate"))
		{
			switch (type)
			{
				case IngredientTypes.Patty:
					// if a patty is cooked but not burnt
					if (foodScript.cooked && !foodScript.burnt)
					{
						other.transform.parent.GetComponent<Plate> ().AddIngredient (this);
					}
					break;

				case IngredientTypes.Lettuce:
					// if lettuce is chopped
					if (foodScript.chopped)
					{
						other.transform.parent.GetComponent<Plate> ().AddIngredient (this);
					}
					break;

				case IngredientTypes.Tomato:
					// if tomato is chopped
					if (foodScript.chopped)
					{
						other.transform.parent.GetComponent<Plate> ().AddIngredient (this);
					}
					break;

				default:
					// default ingredient behavior
					other.transform.parent.GetComponent<Plate> ().AddIngredient (this);
					break;
			}
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
