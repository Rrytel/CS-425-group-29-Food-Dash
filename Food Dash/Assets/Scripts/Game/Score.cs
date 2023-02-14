using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
	int score = 0;

	/*
	*	add to score after turning in an order
	*	increment: the original price of the order
	*	penalty: how long it took to serve the order
	*	multiplier: how accurate the order is
	*/
	public void UpdateScore (Order served)
	{
		int increment = served.GetPrice () * 100;
		int penalty = served.GetLifetime ();
		float multiplier = served.DetermineAccuracy ();

		// (food price * 100 - time penalty) * accuracy
		score = (int) ((increment - penalty) * (multiplier / 100));
	}

	/*
	*	get the score from the order
	*/
	public int GetScore ()
	{
		return score;
	}
}
