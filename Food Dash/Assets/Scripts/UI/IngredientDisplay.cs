using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class IngredientDisplay : MonoBehaviour
{
	Ingredient ingredient;
	TextMeshProUGUI displayText;

	// Start is called before the first frame update
	void Start()
    {
		ingredient = gameObject.GetComponentInParent <Ingredient> ();
		displayText = gameObject.GetComponentInChildren <TextMeshProUGUI> ();
	}

    // Update is called once per frame
    void Update()
    {
		displayText.text = IngredientString ();
    }

	string IngredientString ()
	{
		return ingredient.type.ToString ();
	}
}
