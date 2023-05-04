using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bait_script : MonoBehaviour
{
    GameObject[] rats;
    GameObject baitController;
    float timer;
    public Image LoadingBar;
    public Image CompleteMarker;
    public GameObject[] player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("player mark");
        //Debug.Log("Num player:" + player.Length);
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
        //Fix timer position
        LoadingBar.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + .5f + 0.064f, gameObject.transform.position.z);
        //CompleteMarker.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + .5f, gameObject.transform.position.z);
        //Fix timer rotation
        LoadingBar.transform.LookAt(player[0].transform);
        LoadingBar.transform.eulerAngles = new Vector3(0, LoadingBar.transform.eulerAngles.y, 0);
        //CompleteMarker.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y, 0);


        timer -= .5f * Time.deltaTime;
        LoadingBar.fillAmount = timer / 10f;
        if (timer < 0)
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
