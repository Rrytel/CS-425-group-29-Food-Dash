using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class TimeDisplay : MonoBehaviour
{
	public GameObject gameController;

	Round roundTracker;
	TextMeshProUGUI displayText;

    // Start is called before the first frame update
    void Start ()
    {
		roundTracker = gameController.GetComponent <Round> ();
		displayText = gameObject.GetComponentInChildren <TextMeshProUGUI> ();
	}

    // Update is called once per frame
    void Update ()
    {
		int timeLeft = roundTracker.GetTimeLeft ();

		if (timeLeft < 60)
		{
			displayText.text = timeLeft.ToString ();
		}
		else
		{
			displayText.text = "Day " + roundTracker.day.ToString ();
		}
    }
}
