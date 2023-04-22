using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*	unprepared food code
*/
public class Plate : MonoBehaviour
{
	public FoodTypes type = FoodTypes.Burger;
	public GameObject foodPrefab;

	bool spawnedFood = false;
	List <IngredientTypes> ingredients = new ();

	// Start is called before the first frame update
	void Start ()
    {
		DefineIngredients ();
		foodPrefab.GetComponent <Food> ().type = type;
	}

    // Update is called once per frame
    void Update ()
    {
		foodPrefab.transform.position = this.gameObject.transform.position;

		if (ingredients.Count == 0 && spawnedFood == false)
		{
			Instantiate (foodPrefab);
			spawnedFood = true;
			Destroy (this.gameObject);
		}
    }

	/*
	*   creates a food object of a given type
	*   define the ingredients needed for each food
	*/
	void DefineIngredients ()
	{
		if (type == FoodTypes.Burger)
		{
			ingredients.Add (IngredientTypes.Bun);
			ingredients.Add (IngredientTypes.Patty);
			ingredients.Add (IngredientTypes.Lettuce);
			ingredients.Add (IngredientTypes.Tomato);
			ingredients.Add (IngredientTypes.Bun);
			
		}
		else if (type == FoodTypes.Cheeseburger)
		{
			ingredients.Add (IngredientTypes.Bun);
			ingredients.Add (IngredientTypes.Patty);
			ingredients.Add (IngredientTypes.Cheese);
			ingredients.Add (IngredientTypes.Lettuce);
			ingredients.Add (IngredientTypes.Tomato);
			ingredients.Add (IngredientTypes.Bun);
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

				// destroy the gameobject associated with the ingredient
				Destroy (newIngredient.gameObject);

				return true;
			}
		}

		return false;
	}

	/*
	*	returns a list of the unfulfilled ingredients
	*/
	public List <IngredientTypes> GetIngredients ()
	{
		return ingredients;
	}
}
