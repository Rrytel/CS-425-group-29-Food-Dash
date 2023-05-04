using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class MoodDisplay : MonoBehaviour
{
	Customer customer;
	TextMeshProUGUI displayText;

    // Start is called before the first frame update
    void Start ()
    {
		customer = gameObject.GetComponentInParent <Customer> ();
		displayText = gameObject.GetComponentInChildren <TextMeshProUGUI> ();
    }

    // Update is called once per frame
    void Update ()
    {
		displayText.text = MoodString ();
    }

	string MoodString ()
	{
		string output = "";

		switch (customer.GetMood ())
		{
			case Moods.Happy:
				output = "\U0001F600";
				break;

			case Moods.Impatient:
				// impatient emoji doesn't display with this font
				output = /* "\U0001F928" */ "Impatient";
				break;

			case Moods.Upset:
				// upset emoji doesn't display with this font
				output = /* "\U0001F620" */ "Upset";
				break;
		}

		return output;
	}
}
