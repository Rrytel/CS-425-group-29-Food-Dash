using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ScoreDisplay : MonoBehaviour
{
	public GameObject gameController;

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
		displayText.text = "$" + scoring.GetScore ().ToString ();
    }
}
