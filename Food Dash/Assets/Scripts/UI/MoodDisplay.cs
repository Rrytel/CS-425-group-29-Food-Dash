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
		return customer.GetMood().ToString();
	}
}
