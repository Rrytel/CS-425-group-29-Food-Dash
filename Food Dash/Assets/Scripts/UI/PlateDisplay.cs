using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class PlateDisplay : MonoBehaviour
{
	Plate plate;
	TextMeshProUGUI displayText;

    // Start is called before the first frame update
    void Start()
    {
		plate = gameObject.GetComponentInParent <Plate> ();
		displayText = gameObject.GetComponentInChildren <TextMeshProUGUI> ();
    }

    // Update is called once per frame
    void Update()
    {
		displayText.text = PlateString ();
    }

	string PlateString ()
	{
		return plate.type.ToString ();
	}
}
