using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
	char grade;
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
		float multiplier;

		// inspector grade
		switch (GameObject.FindGameObjectsWithTag ("Rat").Length)
		{
			case 0:
				grade = 'A';
				multiplier = 1.5f;
				break;

			case 1:
				grade = 'B';
				multiplier = 1.25f;
				break;

			case 2:
				grade = 'C';
				multiplier = 1;
				break;

			case 3:
				grade = 'D';
				multiplier = 0.75f;
				break;

			default:
				grade = 'F';
				multiplier = 0.5f;
				break;
		}

		return (int) (score * multiplier);
	}

	public char GetGrade ()
	{
		return grade;
	}

	public void ResetScore ()
	{
		score = 0;
	}
}
