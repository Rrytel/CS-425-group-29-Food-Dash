using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bait_script : MonoBehaviour
{
    GameObject[] rats;
    GameObject baitController;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        baitController = GameObject.FindGameObjectWithTag("BaitController");
        baitController.GetComponent<bait_controller>().AddBait(gameObject);
	  timer = 10f;
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
	  timer -= .5f * Time.deltaTime;
        if(timer < 0)
	  {
		//Play animation and destroy
		Destroy(gameObject);
        }
        /*
        rats = GameObject.FindGameObjectsWithTag("Rat");
        foreach (GameObject rat in rats)
        {
            rat.GetComponent<rat_script>().AddBait(gameObject);
        }
        */
    }
}
