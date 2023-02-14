using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
	// day 0: tutorial
	public int day = 1;
	// how long each round will last (in seconds)
	public int timeLimit = 300;

	public GameObject customerPrefab;

	// score needed to advance to the next day
	int minScore;
	int score = 0;

	// how many customers will visit during this round
	int totalCustomers;
	// how many customers can be in the restuarant at the same time
	int maxCustomers;
	// how often customer spawn attempts occur
	int spawnFrequency;

	float spawnTimer = 0;
	float roundTimer = 0;

	List <Customer> currentCustomers = new ();

	void Start ()
	{
		/*
		*	load the prefab file
		*	http://answers.unity.com/answers/674316/view.html
		*/
		//customerPrefab = Resources.Load <GameObject> (@"..\Prefabs\Game\Customer");

		NewRound();
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

		if (CanSpawnCustomer ())
		{
			/*
			*	events that take place once a customer spawns
			*/
		}
	}

	void NewRound ()
	{
		// remove customers from the previous round
		while (currentCustomers.Count > 0)
		{
			RemoveCustomer (currentCustomers [0]);
		}

		// reset round properties
		spawnTimer = 0;
		roundTimer = 0;
		score = 0;

		// set new round properties
		totalCustomers = day + Random.Range (0, day);
		maxCustomers = totalCustomers / day;
		spawnFrequency = timeLimit / 100;
		// minimum score (needs tuning)
		minScore = totalCustomers * 100;
	}

	/*
	*	attempts to spawn a customer
	*	returns true on success
	*/
	bool CanSpawnCustomer ()
	{
		if (currentCustomers.Count < maxCustomers && spawnTimer > spawnFrequency)
		{
			currentCustomers.Add (Instantiate (customerPrefab).GetComponent <Customer> ());

			spawnTimer = 0;

			return true;
		}

		return false;
	}

	/*
	*	attempts to remove a customer
	*	occurs if a customer is served or if they leave (time out)
	*	returns true on success
	*/
	void RemoveCustomer (Customer toRemove)
	{
		currentCustomers.Remove (toRemove);
		score += toRemove.Leave ();

		totalCustomers -= 1;
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
		return score >= minScore;
	}
}
