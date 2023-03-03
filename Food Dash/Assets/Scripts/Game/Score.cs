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
	*/
	public void UpdateScore (Order served)
	{
		int increment = served.GetPrice () * 10;
		int penalty = served.GetLifetime ();

		score += increment - penalty;

		//print ("Order value: " + served.GetPrice ());
		//print ("Order penalty: " + served.GetLifetime ());
	}

	/*
	*	get the score from the order
	*/
	public int GetScore ()
	{
		return score;
	}

	public void ResetScore ()
	{
		score = 0;
	}
}
