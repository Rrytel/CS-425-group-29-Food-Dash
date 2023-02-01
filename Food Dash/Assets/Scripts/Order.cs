using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
	public int minItems = 1;
	public int maxItems = 3;

	// if the order has been served to the customer
	bool served = false;
	int price = 0;
	// how long the order has existed (in seconds)
	int lifetime = 0;
	float timer;

	// food or drinks in the order
	List <FoodTypes> items;
	List <FoodTypes> currentItems;

	void Start ()
	{
		CreateOrder ();
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if (timer > 1 && served == false)
		{
			lifetime += 1;

			timer -= 1;
		}
	}

	/*
	*	create an order of one or multiple items
	*/
	void CreateOrder ()
	{
		// add items to order and calculate order price
		for (int iterations = 0; iterations <= Random.Range (minItems, maxItems); iterations += 1)
		{
			// in the future: add random food
			items.Add (FoodTypes.Burger);
		}

		// copy list for comparison later
		currentItems = items;
    }

	/*
	*	attempt to fulfill an item in the order
	*/
	bool AddFood (Food newFood)
	{
		for (int index = 0; index < currentItems.Count; index += 1)
		{
			if (currentItems [index].Equals (newFood.type))
			{
				// if the food is in the order, remove from the list
				currentItems.RemoveAt (index);

				return true;
			}
		}

		return false;
	}

	/*
	*	how close the order is to expectations (as a percent)
	*/
	public float determineAccuracy ()
	{
		float accuracy;

		if (items.Count == 0)
		{
			accuracy = 1;
		}
		else
		{
			accuracy = items.Count - currentItems.Count / items.Count;
		}

		return accuracy * 100;
	}

	/*
	*	get the price of the order
	*	order price is the sum of food prices
	*/
	public int GetPrice()
	{
		foreach (FoodTypes foodType in items)
		{
			if (foodType == FoodTypes.Pizza)
			{
				price += 3;
			}
			else if (foodType == FoodTypes.Burger)
			{
				price += 5;
			}
			else if (foodType == FoodTypes.Fries)
			{
				price += 1;
			}
			else if (foodType == FoodTypes.Drink)
			{
				price += 1;
			}
		}

		return price;
	}

	/*
	*	get the lifetime order
	*/
	public int GetLifetime ()
	{
		return lifetime;
	}

	void Serve ()
	{
		served = true;
	}
}
