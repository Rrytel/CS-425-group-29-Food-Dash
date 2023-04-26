using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
	public int minItems = 1;
	public int maxItems = 2;

	// if the order has been served to the customer
	bool served = false;
	int price = 0;
	// how long the order has existed (in seconds)
	int lifetime = 0;
	float timer;

	Score scoring;

	// food or drinks in the order
	List <FoodTypes> items = new ();
	List <FoodTypes> currentItems = new ();

	void Start ()
	{
		scoring = GetComponentInParent <Score> ();

		CreateOrder ();
		UpdatePrice ();
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
					items.Add (FoodTypes.Drink);
					break;

				case 1:
					items.Add (FoodTypes.Burger);
					break;

				case 2:
					items.Add (FoodTypes.Cheeseburger);
					// if you order cheeseburger, do not order a burger
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
	void UpdatePrice ()
	{
		for (int index = 0; index < items.Count; index += 1)
		{
			//print ("Item " + index + ": " + items [index].ToString ());

			if (items [index].Equals (FoodTypes.Burger))
			{
				price += 5;
			}
			else if (items [index].Equals (FoodTypes.Cheeseburger))
			{
				price += 6;
			}
			else if (items [index].Equals (FoodTypes.Drink))
			{
				price += 1;
			}
		}
	}

	public int GetPrice ()
	{
		return price;
	}

	public bool GetServed ()
	{
		return served;
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

	public void SetItems (List <FoodTypes> newItems)
	{
		items = newItems;
	}

	public void Serve ()
	{
		scoring.UpdateScore (this);
		served = true;
	}
}
