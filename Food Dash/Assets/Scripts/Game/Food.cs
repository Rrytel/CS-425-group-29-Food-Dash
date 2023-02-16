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
		// food starts invisible
		GetComponent <MeshRenderer> ().enabled = false;
		DefineIngredients ();

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

	/*
	*   creates a food object of a given type
	*   define the ingredients needed for each food
	*/
	void DefineIngredients ()
	{
		if (type == FoodTypes.Pizza)
		{
			ingredients.Add (IngredientTypes.Dough);
			ingredients.Add (IngredientTypes.Sauce);
			ingredients.Add (IngredientTypes.Cheese);
		}
		else if (type == FoodTypes.Burger)
		{
			ingredients.Add (IngredientTypes.Bun);
			ingredients.Add (IngredientTypes.Patty);
			ingredients.Add (IngredientTypes.Cheese);
			ingredients.Add (IngredientTypes.Lettuce);
			ingredients.Add (IngredientTypes.Bun);
		}
		else if (type == FoodTypes.Fries)
		{
			ingredients.Add (IngredientTypes.Potato);
			ingredients.Add (IngredientTypes.Salt);
		}
		else if (type == FoodTypes.Drink)
		{
			ingredients.Add (IngredientTypes.Beverage);
			ingredients.Add (IngredientTypes.Ice);
			ingredients.Add (IngredientTypes.Cup);
		}
	}

	/*
	*   attempts to fulfill a food's ingredient
	*/
	public bool AddIngredient (Ingredient newIngredient)
	{
		// search for ingredient in list
		for (int index = 0; index < ingredients.Count; index += 1)
		{
			// if found, remove that ingredient from the list
			if (ingredients [index].Equals (newIngredient.type))
			{
				ingredients.RemoveAt (index);

				/*
				*	food is invisible until all ingredients are added
				*	http://answers.unity.com/answers/7779/view.html
				*	can change this code to show an empty plate in the future
				*/
				GetComponent <MeshRenderer> ().enabled = ingredients.Count == 0;

				// destroy the gameobject associated with the ingredient
				Destroy (newIngredient.gameObject);

				return true;
			}
		}

		return false;
	}
}

public enum FoodTypes
{
	Pizza,
	Burger,
	Fries,
	Drink
}
