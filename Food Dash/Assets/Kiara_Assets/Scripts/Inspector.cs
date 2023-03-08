using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inspector : MonoBehaviour
{
    private float time;
    private string grade = "";
    TextMeshProUGUI inspectorText;
    // Start is called before the first frame update
    void Start()
    {
        time = Time.time;
        inspectorText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        grade = "A";
    }

    // Update is called once per frame
    void Update()
    {
        //if 45 seconds have passed, give an inspection
        if (Time.time >= time + 80f)
        {
            giveInspection();
        }

        //if giveInspection
        inspectorText.text = "Inspection: " + grade;

    }

    string giveInspection()
    {
        if (GameObject.FindGameObjectsWithTag("Rat").Length == 0)
        {
            // Do somet$$anonymous$$ng
            grade = "A";
        }else if(GameObject.FindGameObjectsWithTag("Rat").Length == 1)
        {
            grade = "B";
        }
        else if (GameObject.FindGameObjectsWithTag("Rat").Length == 2)
        {
            grade = "C";
        }
        else if (GameObject.FindGameObjectsWithTag("Rat").Length == 3)
        {
            grade = "D";
        }
        else
        {
            grade = "F";
        }
        return grade;
    }
  
}
