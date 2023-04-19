using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bait_script : MonoBehaviour
{
    GameObject[] rats;
    GameObject baitController;
    // Start is called before the first frame update
    void Start()
    {
        baitController = GameObject.FindGameObjectWithTag("BaitController");
        baitController.GetComponent<bait_controller>().AddBait(gameObject);
    }

    private void Awake()
    {
        
        
    }

    private void OnDestroy()
    {
        baitController.GetComponent<bait_controller>().RemoveBait(gameObject);
        /*rats = GameObject.FindGameObjectsWithTag("Rat");
        foreach (GameObject rat in rats)
        {
            rat.GetComponent<rat_script>().RemoveBait(gameObject);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        /*
        rats = GameObject.FindGameObjectsWithTag("Rat");
        foreach (GameObject rat in rats)
        {
            rat.GetComponent<rat_script>().AddBait(gameObject);
        }
        */
    }
}
