using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControls : MonoBehaviour
{
    public GameObject pauseScreen;

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetButtonDown ("Cancel"))
        {
            pauseScreen.SetActive (true);

            Time.timeScale = 0;
        }
    }
}
