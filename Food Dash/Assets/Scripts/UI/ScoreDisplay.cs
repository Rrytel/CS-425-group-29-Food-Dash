using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ScoreDisplay : MonoBehaviour
{
	public GameObject gameController;

	int currentScore;
	// the player's score the last time update was called
	int previousScore = -1;
	Score scoring;
	TextMeshProUGUI displayText;

	// Start is called before the first frame update
	void Start ()
    {
		scoring = gameController.GetComponent <Score> ();
		displayText = gameObject.GetComponentInChildren <TextMeshProUGUI> ();
	}

    // Update is called once per frame
    void Update ()
    {
		currentScore = scoring.GetScore ();

		// if the player's score has changed
		if (currentScore != previousScore)
		{
			StartCoroutine (DisplayScoreUpdate ());
		}

		displayText.text = "$" + currentScore.ToString ();
		previousScore = currentScore;
    }

	/*
	*	displays the updated score and fades out
	*	(persistent ui elements are not good for vr)
	*/
	IEnumerator DisplayScoreUpdate ()
	{
		float fadeSpeed = 0.5f;
		float transparency = 1;

		while (transparency > 0)
		{
			transparency -= fadeSpeed * Time.deltaTime;
			displayText.alpha = transparency;

			yield return null;
		}
	}
}
