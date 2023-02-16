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
		string display = "Invalid order";
		List<FoodTypes> orderItems = customer.GetOrderItems();

		if (orderItems.Count > 0)
		{
			display = "I'd like a ";

			if (orderItems.Count > 1)
			{
				for (index = 0; index < orderItems.Count - 1; index += 1)
				{
					display += orderItems [index].ToString () + ", ";
				}

				display += "and " + orderItems [index].ToString () + ".";
			}
			else
			{
				display += orderItems [0].ToString () + ". ";
			}
		}

		return display;
	}
}
