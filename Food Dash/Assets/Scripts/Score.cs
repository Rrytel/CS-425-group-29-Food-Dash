using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
	int score;

	/*
	*	add to score after turning in an order
	*	increment: the original price of the order
	*	penalty: how long it took to serve the order
	*	multiplier: how accurate the order is
	*/
	void UpdateScore (Order served)
	{
		int increment = served.GetPrice () * 100;
		int penalty = served.GetLifetime ();
		float multiplier = served.determineAccuracy ();

		score += (increment - penalty) *  (int) (multiplier / 100);
	}

	/*
	*	get the current score
	*/
	int GetScore ()
	{
		return score;
	}
}
