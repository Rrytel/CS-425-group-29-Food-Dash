using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
	// how long the customer will wait for their order to complete (in seconds)
	public int patience = 120;
	// how close to expectations the order needs to be to (as a percent)
	public int strictness = 70;

	float timePresent = 0;
	string originalName;
	Moods mood = Moods.Happy;
	Order order;
	GameObject gameController;

	void Start ()
	{
		originalName = name;
		order = gameObject.GetComponent <Order> ();
		gameController = GameObject.Find ("Game Controller");
	}

	void Update ()
	{
		timePresent += Time.deltaTime;

		if (timePresent > patience)
		{
			mood = Moods.Impatient;
		}

		if (order.GetServed () && order.DetermineAccuracy () < strictness)
		{
			mood = Moods.Upset;
		}

		if (order.GetCurrentItems ().Count < 1)
		{
			gameController.GetComponent <Round> ().RemoveCustomer (this);
		}

		name = originalName + " (" + mood.ToString() + ")";
	}

	public Moods GetMood ()
	{
		return mood;
	}

	public Order GetOrder ()
	{
		return order;
	}

	public List <FoodTypes> GetOrderItems ()
	{
		return order.GetCurrentItems ();
	}

	/*
	*	the process for leaving the restuarant
	*/
	public void Leave ()
	{
		/*
		*	customer travels towards exit
		*	maybe: leave a review depending on mood that affects score?
		*/

		order.Serve ();
		Destroy (gameObject);
	}
}

public enum Moods
{
	Happy,
	Impatient,
	Upset
}
