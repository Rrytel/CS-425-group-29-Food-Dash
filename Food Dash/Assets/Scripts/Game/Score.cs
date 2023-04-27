using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
	float inspectorGrade;
	int score = 0;

	/*
	*	add to score after turning in an order
	*	increment: the original price of the order
	*	penalty: how long it took to serve the order
	*/
	public void UpdateScore (Order served)
	{
		int increment = 200;
		int penalty = served.GetLifetime () ;

		score += increment - penalty;

		//print ("Order value: " + served.GetPrice ());
		//print ("Order penalty: " + served.GetLifetime ());
	}

	/*
	*	get the score from the order
	*/
	public int GetScore ()
	{
		// inspector grade
		var multiplier = GameObject.FindGameObjectsWithTag ("Rat").Length switch
		{
			// a
			0 => 1.5f,
			// b
			1 => 1.25f,
			// c
			2 => 1,
			// d
			3 => 0.75f,
			// f
			_ => 0.5f,
		};

		return (int) (score * multiplier);
	}

	public void ResetScore ()
	{
		score = 0;
	}
}
