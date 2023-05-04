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

		displayText.text = timeLeft.ToString ();

		// show the time every 15 seconds
		if (timeLeft % 30 == 0 || timeLeft == 5)
		{
			StartCoroutine (DisplayTimeUpdate ());
		}
    }

	IEnumerator DisplayTimeUpdate ()
	{
		float fadeSpeed = 0.25f;
		float transparency = 1;

		while (transparency > 0)
		{
			transparency -= fadeSpeed * Time.deltaTime;
			displayText.alpha = transparency;

			yield return null;
		}
	}
}
