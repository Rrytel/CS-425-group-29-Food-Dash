using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using TMPro;

public class Setting : MonoBehaviour
{
    public TMP_InputField textInput;
    public Scrollbar scrollbar;

    public int defaultValue;
    public int minimumValue;
    public int maximumValue;

    int value;

    // Start is called before the first frame update
    void Start ()
    {
        textInput.onEndEdit.AddListener 
        (
            delegate 
            {
                SetValue ("InputField");
            }
        );
        scrollbar.onValueChanged.AddListener
        (
            delegate
            {
                SetValue ("Scrollbar");
            }
        );

        value = defaultValue;
        textInput.text = value.ToString();
        scrollbar.value = (float) value / 100;
    }

    // Update is called once per frame
    void Update ()
    {
        //print (name + ": " + value);
    }

    void SetValue (string source)
    {
        switch (source)
        {
            case "InputField":
                int.TryParse(textInput.text, out value);
                Clamp ();
                

                break;

            case "Scrollbar":
                value = (int) (scrollbar.value * 100.0);
                Clamp ();
                

                break;
        }

        Clamp ();

        textInput.text = value.ToString();
        scrollbar.value = (float)value / 100;
    }

    void Clamp ()
    {
        if (value < minimumValue)
        {
            value = minimumValue;
        }

        if (value > maximumValue)
        {
            value = maximumValue;
        }
    }

    public int GetValue ()
    {
        return value;
    }
}
