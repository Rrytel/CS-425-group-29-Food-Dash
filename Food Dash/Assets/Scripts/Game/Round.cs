using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;

public class Round : MonoBehaviour
{
	// the level that the scene is associated with
	public int level;
	// how long each round will last (in seconds)
	public int timeLimit = 240;
	public GameObject customerPrefab;
	public GameObject gameUI;
	public GameObject victoryScreen;
	public GameObject defeatScreen;
	public GameObject gameOverScreen;
	public GameObject rayInteractor;
	
	Score scoring;
	static int day = 0;
	// score needed to advance to the next day
	int minScore;
	// the number of unique scenes to loop through
	int levels = 2;
	// how many customers will visit during this round
	int totalCustomers;
	// how many customers can be in the restuarant at the same time
	int maxCustomers;
	// how often customer spawn attempts occur
	/* int spawnFrequency; */
	int timeLeft;
	/* float spawnTimer = 0; */
	float roundTimer = 0;
	List<Customer> currentCustomers = new ();
	// time between inspection fail and reset to main menu
	float gameResetTimer = 5;

	void Start ()
	{
		scoring = GetComponent<Score> ();

		// set first round properties (day 0 tutorial)
		totalCustomers = 1;
		maxCustomers = 1;
		/* spawnFrequency = timeLimit / 100; */
		// minimum score (needs tuning)
		minScore = totalCustomers * 5;

		Time.timeScale = 1;
	}

	void Update ()
	{
		/* spawnTimer += Time.deltaTime; */
		roundTimer += Time.deltaTime;
		timeLeft = (int) (timeLimit - roundTimer);

		if (RoundIsOver ())
		{
			Time.timeScale = 0;

			gameUI.SetActive (false);

			// if round was won
			if (EndRound () == 1)
			{
				victoryScreen.GetComponentInChildren<TextMeshProUGUI> ().text = "Day " + day.ToString () + " success! ";
				victoryScreen.GetComponentInChildren<TextMeshProUGUI> ().text += "You earned $" + scoring.GetScore ().ToString () + " today ";
				victoryScreen.GetComponentInChildren<TextMeshProUGUI> ().text += "with inspection grade " + scoring.GetGrade ().ToString () + ".";
				victoryScreen.SetActive (true);
				rayInteractor.SetActive (true);
			}
			// if round was lost
			else if (EndRound () == 0)
			{
				defeatScreen.GetComponentInChildren<TextMeshProUGUI> ().text = "Day " + day.ToString () + " fail! ";
				defeatScreen.GetComponentInChildren<TextMeshProUGUI> ().text += "You earned $" + scoring.GetScore ().ToString () + " today ";
				defeatScreen.GetComponentInChildren<TextMeshProUGUI> ().text += "with inspection grade " + scoring.GetGrade ().ToString () + ", ";
				defeatScreen.GetComponentInChildren<TextMeshProUGUI> ().text += "but need " + minScore.ToString () + " to win.";
				defeatScreen.SetActive (true);
				rayInteractor.SetActive (true);
			}
			// true game over screen
			else
			{
				gameOverScreen.SetActive (true);
				Time.timeScale = 1;
				SceneManager.LoadScene ("Main Menu");
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
			day += 1;
			level += 1;
		}

		if (level > levels)
		{
			level = 1;
		}

		switch (level)
		{
			// level 1
			case 1:
				SceneManager.LoadScene ("food_dash_aaron");
				break;

			// level 2
			case 2:
				SceneManager.LoadScene ("food_dash");
				break;

			// level 0 (or any other day)
			default:
				SceneManager.LoadScene ("food_dash_tutorial");
				break;
		}

		// remove customers from the previous round
		while (currentCustomers.Count > 0)
		{
			RemoveCustomer (currentCustomers [0]);
		}

		// reset round properties
		/* spawnTimer = 0; */
		roundTimer = 0;
		scoring.ResetScore ();

		// set new round properties
		totalCustomers = day;
		maxCustomers = 1;
		// minimum score (needs tuning)
		minScore = totalCustomers * 5;

		// disable ray interactor (ui controller)
		rayInteractor.SetActive (false);
		// enable ui
		gameUI.SetActive (true);

		Time.timeScale = 1;
	}

	/*
	*	attempts to spawn a customer
	*	returns true on success
	*/
	bool CanSpawnCustomer ()
	{
		if (currentCustomers.Count < maxCustomers /* && spawnTimer > spawnFrequency */)
		{
			currentCustomers.Add (Instantiate (customerPrefab, GetComponent<Transform> ()).GetComponent<Customer> ());

			/* spawnTimer = 0; */

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
	int EndRound ()
	{
		/*
		*	stop gameplay
		*	maybe: save stats
		*/

		// if there are too many rats present at the end of the round
		if (scoring.GetGrade () == 'F')
		{
			// true game over screen, the user will have to start from day 1
			day = 1;
			level = 1;

			return -1;
		}

		// if minimum score is achieved
		if (scoring.GetScore () >= minScore)
		{
			return 1;
		}
		else
		{
			return 0;
		}
	}

	public int GetDay ()
	{
		return day;
	}

	public int GetTimeLeft ()
	{
		return timeLeft;
	}
}
