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

	void Start ()
	{
		originalName = name;
		order = gameObject.GetComponent <Order> ();
	}

	void Update ()
	{
		timePresent += Time.deltaTime;

		if (timePresent > patience)
		{
			mood = Moods.Impatient;
		}

		if (order.DetermineAccuracy () < strictness)
		{
			mood = Moods.Upset;
		}

		if (order.GetCurrentItems ().Count < 1)
		{
			Leave ();
		}

		name = originalName + " (" + mood.ToString() + ")";
	}

	public Moods GetMood ()
	{
		return mood;
	}

	public List <FoodTypes> GetOrderItems ()
	{
		return order.GetItems ();
	}

	/*
	*	the process for leaving the restuarant
	*/
	public int Leave ()
	{
		/*
		*	customer travels towards exit
		*	maybe: leave a review depending on mood that affects score?
		*/

		int orderScore = order.Serve ();

		Destroy (gameObject);

		return orderScore;
	}
}

public enum Moods
{
	Happy,
	Impatient,
	Upset
}
