using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI text;

    public int timerDuration;

    int secondsLeft;
    int minutes;
    int seconds;
    float timePassed;

    // Start is called before the first frame update
    void Start ()
    {
        secondsLeft = timerDuration;
    }

    // Update is called once per frame
    void Update()
    {
        minutes = secondsLeft / 60;
        seconds = secondsLeft % 60;

        timePassed += Time.deltaTime;

        if (timePassed >= 1)
        {
            timePassed -= 1;

            secondsLeft -= 1;

            if (secondsLeft < 0)
            {
                secondsLeft = 0;
            }
        }

        text.text = "";

        if (minutes > 0)
        {
            text.text += minutes + ":";
        }

        if (secondsLeft > 10)
        {
            text.text += FormatSeconds();
        }
        else
        {
            text.text += seconds;
        }
    }

    string FormatSeconds ()
    {
        string secondsString = seconds.ToString ();

        if (secondsString.Length < 2)
        {
            return "0" + secondsString;
        }

        return secondsString;
    }
}
