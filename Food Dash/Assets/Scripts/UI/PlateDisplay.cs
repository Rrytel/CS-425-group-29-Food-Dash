using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PlateDisplay : MonoBehaviour
{
	Plate plate;
	TextMeshProUGUI displayText;

    // Start is called before the first frame update
    void Start()
    {
		plate = gameObject.GetComponentInParent <Plate> ();
		displayText = gameObject.GetComponentInChildren <TextMeshProUGUI> ();
    }

    // Update is called once per frame
    void Update()
    {
		displayText.text = PlateString ();
    }

	/*
	*	creates a string of emojis to represent required ingredients
	*/
	string PlateString ()
	{
		string output = "";

		// the ingredients that the plate still needs
		List <IngredientTypes> currentIngredients = plate.GetIngredients ();

		for (int index = 0; index < currentIngredients.Count; index += 1)
		{
			switch (currentIngredients [index])
			{
				case IngredientTypes.Bun:
					output += "\U0001F35E";
					break;

				case IngredientTypes.Cheese:
					output += "\U0001F9C0";
					break;

				case IngredientTypes.Lettuce:
					output += "\U0001F96C";
					break;

				case IngredientTypes.Patty:
					output += "\U0001F969";
					break;

				case IngredientTypes.Tomato:
					output += "\U0001F345";
					break;
			}
		}

		return output;
	}
}
