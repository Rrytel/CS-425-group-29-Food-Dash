using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;

public class Round : MonoBehaviour
{
	// day 0: tutorial
	public int day = 1;
	// how long each round will last (in seconds)
	public int timeLimit = 300;

	public GameObject customerPrefab;
	public GameObject victoryScreen;
	public GameObject defeatScreen;
	public GameObject rayInteractor;
	Score scoring;

	// score needed to advance to the next day
	int minScore;
	int level;

	// how many customers will visit during this round
	int totalCustomers;
	// how many customers can be in the restuarant at the same time
	int maxCustomers;
	// how often customer spawn attempts occur
	int spawnFrequency;
	int timeLeft;
	float spawnTimer = 0;
	float roundTimer = 0;

	List<Customer> currentCustomers = new ();

	void Start ()
	{
		/*
		*	load the prefab file
		*	http://answers.unity.com/answers/674316/view.html
		*/
		//customerPrefab = Resources.Load <GameObject> (@"..\Prefabs\Game\Customer");
		//victoryScreen = GameObject.Find ("Victory");
		//defeatScreen = GameObject.Find ("Defeat");
		scoring = GetComponent<Score> ();

		// set new round properties
		totalCustomers = day + Random.Range (0, day);
		maxCustomers = 1;
		spawnFrequency = timeLimit / 100;
		// minimum score (needs tuning)
		minScore = totalCustomers * 5;

		Time.timeScale = 1;
	}

	void Update ()
	{
		spawnTimer += Time.deltaTime;
		roundTimer += Time.deltaTime;
		timeLeft = (int) (timeLimit - roundTimer);

		if (RoundIsOver ())
		{
			Time.timeScale = 0;

			// if round was won
			if (EndRound ())
			{
				victoryScreen.GetComponentInChildren<TextMeshProUGUI> ().text = "Day " + day.ToString () + " success!";
				victoryScreen.SetActive (true);
				rayInteractor.SetActive (true);
			}
			// if round was lost
			else
			{
				defeatScreen.GetComponentInChildren<TextMeshProUGUI> ().text = "Day " + day.ToString () + " fail!";
				defeatScreen.SetActive (true);
				rayInteractor.SetActive (true);
			}
		}

		if (CanSpawnCustomer ())
		{
			/*
			*	events that take place once a customer spawns
			*/
		}
	}

	public void NewRound (bool advance)
	{
		// if advancing to the next day
		if (advance)
		{
			/*
			*	for demo:
			*	load new scene for next day
			*/
			day += 1;
			level += 1;
		}

		// go back to first level after completing last
		if (level > 2)
		{
			level = 1;
		}


		switch (level)
		{
			case 1:
				SceneManager.LoadScene ("food_dash_aaron");
				break;

			case 2:
				SceneManager.LoadScene ("food_dash");
				break;

			default:
				SceneManager.LoadScene ("food_dash");
				break;
		}

		// remove customers from the previous round
		while (currentCustomers.Count > 0)
		{
			RemoveCustomer (currentCustomers [0]);
		}

		// reset round properties
		spawnTimer = 0;
		roundTimer = 0;
		scoring.ResetScore ();

		// set new round properties
		totalCustomers = day;
		maxCustomers = 1;
		spawnFrequency = timeLimit / 100;
		// minimum score (needs tuning)
		minScore = totalCustomers * 5;

		// disable ray interactor (ui controller)
		rayInteractor.SetActive (false);

		Time.timeScale = 1;
	}

	/*
	*	attempts to spawn a customer
	*	returns true on success
	*/
	bool CanSpawnCustomer ()
	{
		if (currentCustomers.Count < maxCustomers && spawnTimer > spawnFrequency)
		{
			currentCustomers.Add (Instantiate (customerPrefab, GetComponent<Transform> ()).GetComponent<Customer> ());

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
	public void RemoveCustomer (Customer toRemove)
	{
		currentCustomers.Remove (toRemove);
		toRemove.Leave ();

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
		return scoring.GetScore () >= minScore;
	}

	public int GetTimeLeft ()
	{
		return timeLeft;
	}
}
