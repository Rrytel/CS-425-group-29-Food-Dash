using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testFoodScript : MonoBehaviour
{
    // Start is called before the first frame update
    string target;
    public float cookTimeLeft = 100;
    public GameObject GO;
    public bool cooked = false;
    public bool burnt = false;
    public bool inCookingArea = false;
    MeshFilter meshF;
    
    void Start()
    {
        target = "Box Volume";
        meshF = GO.GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (cookTimeLeft < 0)
        {
            meshF.sharedMesh = Resources.Load<Mesh>("heat_lamp");
            
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        inCookingArea = true;
        if(coll.CompareTag("Test"))
        {
            GO.transform.position = coll.transform.GetChild(0).transform.position;
            coll.transform.parent.GetComponent<stove_controller>().Cook(GO);
        }
        Debug.Log("Teleport");
           
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Test")) 
        {
            //Debug.Log(cookTimeLeft);
            //cookTimeLeft -= 50 * Time.deltaTime;
            
        }
        

    }

    void OnTriggerExit(Collider coll)
    {
        inCookingArea = false;
    }
}
