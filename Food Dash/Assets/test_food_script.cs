using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_food_script : MonoBehaviour
{
    // Start is called before the first frame update
    string target;
    public float cookTimeLeft = 100;
    public GameObject GO;
    public bool cooked = false;
    public bool burnt = false;
    public bool inCookingArea = false;
    public float heat = 0;
    public float cookThresh;
    public float burnThresh;
    MeshFilter meshF;
    public Collider activeArea;
    Rigidbody m_Rigidbody;
    
    void Start()
    {
        //target = "Box Volume";
        meshF = GO.GetComponent<MeshFilter>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Update mesh based on stage in cooking process
        if (heat > cookThresh)
        {
            //Swap for cooked mesh
            meshF.sharedMesh = Resources.Load<Mesh>("heat_lamp");
            //Play sound

        }
        if(heat > burnThresh)
        {
            //Swap for burnt mesh
            meshF.sharedMesh = Resources.Load<Mesh>("sink_handwash");
            //Play sound

        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_Rigidbody.velocity = transform.forward * 5;
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.transform.parent.parent.GetComponent<stove_controller>().occupied == false)
        {
            inCookingArea = true;
            activeArea = coll;
            
            if (coll.CompareTag("Test"))
            {
                //Remove velocity and place in work zone
                GO.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GO.transform.position = coll.transform.GetChild(0).transform.position;

                coll.transform.parent.parent.GetComponent<stove_controller>().ObjectEnter(GO);
                coll.transform.parent.parent.GetComponent<stove_controller>().Cook(GO);
            }
        }
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.CompareTag("Test")) 
        {
            //Keep object in place
           // GO.transform.position = coll.transform.GetChild(0).transform.position;
            
            //Alterante way to add heat
            //cookTimeLeft -= 50 * Time.deltaTime;
            
        }
    }

    void OnTriggerExit(Collider coll)
    {
        if (coll.CompareTag("Test"))
        {
            inCookingArea = false;
            coll.transform.parent.parent.GetComponent<stove_controller>().ObjectExit(GO);
        }
        //activeArea = null;
    }

    public void AddHeat(float amount)
    {
        heat += amount * Time.deltaTime;
    }
}
