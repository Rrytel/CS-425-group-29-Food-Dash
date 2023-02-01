using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public FoodTypes type = FoodTypes.Pizza;

	bool ready = false;

	// stores the ingredients needed to prepare the food
	List <IngredientTypes> ingredients;

	// Start is called before the first frame update
	void Start ()
	{
		defineIngredients ();
	}

	void Update ()
	{
		if (ingredients.Count == 0)
		{
			ready = true;
		}
	}

	/*
	*   creates a food object of a given type
	*   define the ingredients needed for each food
	*/
	void defineIngredients ()
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
	bool AddIngredient (Ingredient newIngredient)
	{
		// search for ingredient in list
		for (int index = 0; index < ingredients.Count; index += 1)
		{
			// if found, remove that ingredient from the list
			if (ingredients [index].Equals (newIngredient.type))
			{
				ingredients.RemoveAt (index);

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
