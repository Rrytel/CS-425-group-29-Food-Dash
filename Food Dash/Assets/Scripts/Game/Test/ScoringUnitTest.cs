using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

/*
*	tests score calculation and display 
*/
public class ScoringUnitTest : MonoBehaviour
{
	// how long until the order gets served
	public int serveDelay;
	public GameObject customerPrefab;
	public GameObject foodPrefab;

	bool justSpawnedCustomer = true;
	bool done = false;
	float delayTimer;
	Score scoreTracker;
	List <FoodTypes> itemsList = new ();

	// Start is called before the first frame update
	void Start ()
	{
		delayTimer = serveDelay;

		scoreTracker = GetComponent <Score> ();

		// setup sample order
		itemsList.Add (FoodTypes.Burger);
		itemsList.Add (FoodTypes.Drink);

		// spawn customer
		Instantiate (customerPrefab, GetComponent <Transform> ());
	}

    // Update is called once per frame
    void Update ()
    {
		if (justSpawnedCustomer)
		{
			GiveOrder ();

			justSpawnedCustomer = false;
		}

		delayTimer -= Time.deltaTime;

		// wait until delay time has elapsed
		if (delayTimer <= 0 && !done)
		{
			// give the order to the customer
			ServeOrder ();

			StartCoroutine (ReportResults ());

			done = true;
		}
    }

	bool TestScore ()
	{
		/*
		*	order value is 7
		*	(burger: 5, fries: 1, drink: 1)
		*	multiply value by 10
		*	subtract result by serveDelay
		*/
		int expected = (7 * 10) - serveDelay;
		int actual = scoreTracker.GetScore ();

		print ("Expected score: " + expected.ToString ());
		print ("Actual score: " + actual.ToString ());

		/*
		*	check if the expected and actual score are close 
		*	+ or - 1 point due to floats 
		*/
		return Mathf.Abs (expected - actual) <= 1;
	}

	void ServeOrder ()
	{
		Customer spawned = gameObject.GetComponentInChildren <Customer> ();

		// spawn each food that the customer asked for
		for (int index = 0; index < itemsList.Count; index += 1)
		{
			foodPrefab.GetComponent <Food> ().type = itemsList [index];
			spawned.GetOrder ().AddFood (foodPrefab.GetComponent <Food> ());
		}
	}

	void GiveOrder ()
	{
		Customer spawned = gameObject.GetComponentInChildren <Customer> ();

		spawned.GetOrder ().SetItems (itemsList);
	}

	IEnumerator ReportResults ()
	{
		// wait a second since serving food happens over time
		yield return new WaitForSeconds (1);

		// see if the expected score matches the actual score
		if (TestScore ())
		{
			print ("Scoring unit test passed");
		}
		else
		{
			print ("Scoring unit test failed");
		}
	}
}
