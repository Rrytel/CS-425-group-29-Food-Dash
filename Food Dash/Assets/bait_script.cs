using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bait_script : MonoBehaviour
{
    GameObject[] rats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        
        
    }

    private void OnDestroy()
    {
        rats = GameObject.FindGameObjectsWithTag("Rat");
        foreach (GameObject rat in rats)
        {
            rat.GetComponent<rat_script>().RemoveBait(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        rats = GameObject.FindGameObjectsWithTag("Rat");
        foreach (GameObject rat in rats)
        {
            rat.GetComponent<rat_script>().AddBait(gameObject);
        }
    }
}
