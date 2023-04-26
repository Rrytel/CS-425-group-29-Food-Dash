using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
	public int spawnFrequency = 3;
	public GameObject ingredientPrefab;

	float timer = 0;
	int iteration = 0;

   // Update is called once per frame
    void Update()
    {
		timer += Time.deltaTime;

		if (timer > spawnFrequency)
		{
			timer = 0;

			switch (iteration)
			{
				case 0:
					ingredientPrefab.GetComponent<Ingredient> ().type = IngredientTypes.Dough;
					break;

				case 1:
					ingredientPrefab.GetComponent<Ingredient> ().type = IngredientTypes.Sauce;
					break;

				case 2:
					ingredientPrefab.GetComponent<Ingredient> ().type = IngredientTypes.Bun;
					break;

				case 3:
					ingredientPrefab.GetComponent<Ingredient> ().type = IngredientTypes.Patty;
					break;

				case 4:
					ingredientPrefab.GetComponent<Ingredient> ().type = IngredientTypes.Cheese;
					break;

				case 5:
					ingredientPrefab.GetComponent<Ingredient> ().type = IngredientTypes.Lettuce;
					break;

				case 6:
					ingredientPrefab.GetComponent<Ingredient> ().type = IngredientTypes.Potato;
					break;

				case 7:
					ingredientPrefab.GetComponent<Ingredient> ().type = IngredientTypes.Salt;
					break;

				case 8:
					ingredientPrefab.GetComponent<Ingredient> ().type = IngredientTypes.Beverage;
					break;

				case 9:
					ingredientPrefab.GetComponent<Ingredient> ().type = IngredientTypes.Ice;
					break;

				case 10:
					ingredientPrefab.GetComponent<Ingredient> ().type = IngredientTypes.Cup;
					break;
			}

			Instantiate (ingredientPrefab, this.gameObject.transform);

			iteration += 1;

			if (iteration > 10)
			{
				iteration = 0;
			}	
		}
    }
}
