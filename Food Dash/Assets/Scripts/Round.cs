using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
	// day 0: tutorial
	public int day = 1;
	// how long each round will last (in seconds)
	public int timeLimit = 300;

	// score needed to advance to the next day
	int minScore;

	// how many customers are currently in the restuarant
	int currentCustomers = 0;
	// how many customers will visit during this round
	int totalCustomers;
	// how many customers can be in the restuarant at the same time
	int maxCustomers;
	// how often customer spawn attempts occur
	int spawnFrequency;

	float spawnTimer = 0;
	float roundTimer = 0;

	Score roundScore;

	GameObject customerPrefab;

	void Start ()
	{
		roundScore = gameObject.GetComponent <Score> ();
		// http://answers.unity.com/answers/674316/view.html
		customerPrefab = Resources.Load <GameObject> (@"..\Prefabs\Game\Customer");

		NewRound ();
	}

	void Update ()
	{
		spawnTimer += Time.deltaTime;
		roundTimer += Time.deltaTime;

		if (RoundIsOver ())
		{
			// if round was won
			if (EndRound ())
			{
				/*
				*	display victory screen
				*	score, time elapsed, inspection rating
				*	3 options: replay round, advance to next day, or go to main menu
				*/
			}
			// if round was lost
			else
			{
				/*
				*	display round lost screen
				*	score, time elapsed, inspection rating
				*	2 options: replay round or go to main menu
				*/
			}
		}

		if (spawnTimer > spawnFrequency)
		{
			SpawnCustomer ();

			spawnTimer = 0;
		}
	}

	void NewRound ()
	{
		// reset round properties
		currentCustomers = 0;
		spawnTimer = 0;
		roundTimer = 0;
		roundScore.ResetScore ();

		// set new round properties
		totalCustomers = day + Random.Range (0, day);
		maxCustomers = totalCustomers / (day / 2);
		spawnFrequency = timeLimit / 100;
		// minimum score (needs tuning)
		minScore = totalCustomers * 100;
	}

	/*
	*	attempts to spawn a customer
	*	returns true on success
	*/
	bool SpawnCustomer ()
	{
		if (currentCustomers < maxCustomers)
		{
			Instantiate (customerPrefab);

			currentCustomers += 1;

			return true;
		}

		return false;
	}

	/*
	*	attempts to remove a customer
	*	occurs if a customer is served or if they leave (time out)
	*	returns true on success
	*/
	bool RemoveCustomer (Customer toRemove)
	{
		if (currentCustomers > 0)
		{
			toRemove.Leave ();

			currentCustomers -= 1;
			totalCustomers -= 1;

			return true;
		}

		return false;
	}

	/*
	*	conditions where the round will end
	*	returns true when the round should end
	*/
	bool RoundIsOver ()
	{
		// if time limit is reached
		if (roundTimer > timeLimit)
		{
			return true;
		}

		// if every customer in the round has visited and left the restuarant
		if (totalCustomers == 0)
		{
			return true;
		}

		return false;
	}

	/*
	*	end the round and determine if the round was a success
	*	returns true on win
	*/
	bool EndRound ()
	{
		/*
		*	stop gameplay
		*	maybe: save stats
		*/

		// if minimum score is achieved
		return roundScore.GetScore () >= minScore;
	}
}
