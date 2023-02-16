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

	Score score;

	// food or drinks in the order
	List <FoodTypes> items = new ();
	List <FoodTypes> currentItems = new ();

	void Start ()
	{
		score = gameObject.GetComponent <Score> ();

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
		// how many items will be in the order
		int itemCount = Random.Range (minItems, maxItems + 1);

		// temporary code for variety in orders
		while (itemCount >= 0)
		{
			switch (itemCount)
			{
				case 0:
					items.Add (FoodTypes.Fries);
					break;

				case 1:
					items.Add (FoodTypes.Drink);
					break;

				case 2:
					items.Add (FoodTypes.Burger);
					break;

				case 3:
					items.Add (FoodTypes.Pizza);
					// if you order pizza, do not order a burger
					itemCount -= 1;
					break;
			}

			itemCount -= 1;
		}

		// copy list for comparison later
		currentItems = items;
    }

	/*
	*	attempt to fulfill an item in the order
	*/
	public bool AddFood (Food newFood)
	{
		for (int index = 0; index < currentItems.Count; index += 1)
		{
			if (currentItems [index].Equals (newFood.type))
			{
				// if the food is in the order, remove from the list
				currentItems.RemoveAt (index);

				// destroy gameobject associated with food
				Destroy (newFood.gameObject);

				return true;
			}
		}

		return false;
	}

	/*
	*	how close the order is to expectations (as a percent)
	*	code just checks how many items are in the served order for now
	*/
	public float DetermineAccuracy ()
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

	public List <FoodTypes> GetItems ()
	{
		return items;
	}

	public List <FoodTypes> GetCurrentItems ()
	{
		return currentItems;
	}

	public int Serve ()
	{
		score.UpdateScore (this);
		served = true;

		return score.GetScore ();
	}
}
