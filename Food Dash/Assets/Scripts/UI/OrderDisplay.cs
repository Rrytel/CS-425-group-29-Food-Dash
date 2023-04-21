using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class OrderDisplay : MonoBehaviour
{
	Customer customer;
	TextMeshProUGUI displayText;

	// Start is called before the first frame update
	void Start()
	{
		customer = gameObject.GetComponentInParent <Customer> ();
		displayText = gameObject.GetComponentInChildren <TextMeshProUGUI> ();
	}

    // Update is called once per frame
    void Update()
    {
		displayText.text = OrderString ();
	}

	/*
	*	customer's order displayed as text
	*/
	string OrderString ()
	{
		int index;
		// invalid order
		string display = "\U0000274C";
		List<FoodTypes> orderItems = customer.GetOrderItems();

		if (orderItems.Count > 0)
		{
			// clear the string
			display = "";

			for (index = 0; index < orderItems.Count - 1; index += 1)
			{
				switch (orderItems [index])
				{
					case FoodTypes.Burger:
						display += "\U0001F354";
						break;
					case FoodTypes.Cheeseburger:
						display += "\U0001F9C0 \U0001F354";
						break;
					case FoodTypes.Drink:
						display += "\U0001F964";
						break;
				}
			}
		}

		return display;
	}
}
