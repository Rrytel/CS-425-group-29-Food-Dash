using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class OrderValue : MonoBehaviour
{
    public TextMeshProUGUI valueDisplay;

    public int ticketLifetime;

    int value;
    int decrement;
    float timePassed;

    // Start is called before the first frame update
    void Start ()
    {
        int multiplier = Random.Range (10, 30);

        value = ticketLifetime * multiplier;
        decrement = value / ticketLifetime;

        //print ("Order price multiplier: " + multiplier);
    }

    // Update is called once per frame
    void Update ()
    {
        timePassed += Time.deltaTime;

        if (timePassed >= 1)
        {
            timePassed -= 1;

            value -= decrement;

            if (value < 0)
            {
                value = 0;
            }
        }

        valueDisplay.text = FormatValue ();
    }

    string FormatValue ()
    {
        int dollars = value / 100;
        string cents = (value % 100).ToString ();

        for (int missingDecimals = 2 - cents.Length; missingDecimals > 0; missingDecimals -= 1)
        {
            cents += "0";
        }

        return "$" + dollars + "." + cents;
    }
}
