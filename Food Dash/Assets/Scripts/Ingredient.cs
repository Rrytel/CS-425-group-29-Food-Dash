using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*   add this script to an object and it will be considered an ingredient
*/
public class Ingredient : MonoBehaviour
{
    public IngredientTypes type  = IngredientTypes.Dough;
}

public enum IngredientTypes
{
    Dough,
    Sauce,
    Bun,
    Patty,
    Cheese,
    Lettuce,
    Onion,
    Tomato,
}
