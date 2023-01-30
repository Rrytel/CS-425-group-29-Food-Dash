using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public FoodTypes type = FoodTypes.Pizza;
    public int price = 5;
    // stores the names of the ingredients needed to prepare the food
    List <string> ingredients;
    bool ready = false;

    // Start is called before the first frame update
    void Start ()
    {
        defineIngredients ();
    }

    private void Update ()
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
        if (type.ToString () == "Pizza")
        {
            ingredients.Add ("Dough");
            ingredients.Add ("Sauce");
            ingredients.Add ("Cheese");
        }
        // burger ingredients by default
        else
        {
            ingredients.Add ("Bun");
            ingredients.Add ("Patty");
            ingredients.Add ("Cheese");
            ingredients.Add ("Lettuce");
            ingredients.Add ("Onion");
            ingredients.Add ("Tomato");
            ingredients.Add ("Bun");
        }
    }

    /*
    *   attempts to add an ingredient to a food
    *   maybe call this when the ingredient touches a plate?
    */
    bool AddIngredient (Ingredient newIngredient)
    {
        // search for ingredient in list
        for (int index = 0; index < ingredients.Count; index += 1)
        {
            // if found, remove that ingredient from the list
            if (ingredients [index] == newIngredient.type.ToString ())
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
    Burger
}