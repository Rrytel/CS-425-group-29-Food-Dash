//Name: Ryan Rytel
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
    public int chopThresh;
    public AudioSource audioSource;
    public AudioClip cookedSound;
    public AudioClip burntSound;
    MeshFilter meshF;
    public Collider activeArea;
    Rigidbody m_Rigidbody;
    int chopState = 0;

    
    void Start()
    {
        meshF = GO.GetComponent<MeshFilter>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Update mesh based on stage in cooking process
        if (heat > cookThresh && cooked == false)
        {
            //Update variables
            cooked = true;
            //Swap for cooked mesh
            meshF.sharedMesh = Resources.Load<Mesh>("heat_lamp");
            //Play sound for cooked food
            audioSource.PlayOneShot(cookedSound, .3f);
        }
        if(heat > burnThresh && burnt == false)
        {
            //Update variables
            burnt = true;
            //Swap for burnt mesh
            meshF.sharedMesh = Resources.Load<Mesh>("sink_handwash");
            //Play sound for burnt food
            audioSource.PlayOneShot(burntSound, .3f);

        }

        //Update mesh based on stage in chopping process
        if(chopState > chopThresh)
        {
            //Update varibles

            //Swap for chopped mesh
            meshF.sharedMesh = Resources.Load<Mesh>("sink_handwash");
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            m_Rigidbody.velocity = transform.forward * 5;
        }
    }

    void OnTriggerEnter(Collider coll)
    {

        switch(coll.tag)
        {
            case "Test":
                /*if (coll.transform.parent.parent.GetComponent<stove_controller>().occupied == false)
                {
                    inCookingArea = true;
                    activeArea = coll;

                    //Remove velocity and place in work zone
                    GO.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    GO.transform.position = coll.transform.GetChild(0).transform.position;
                    //Call stove cooking functions
                    coll.transform.parent.parent.GetComponent<stove_controller>().ObjectEnter(GO);
                    coll.transform.parent.parent.GetComponent<stove_controller>().Cook(GO);
                    
                }*/
                break;

            case "Knife":
                chopState += 1;
                break;
        
        
        }
    }

    void OnTriggerStay(Collider coll)
    {

        switch (coll.tag)
        {
            case "Test":

                //Check to see if stove is busy
                if (coll.transform.parent.parent.GetComponent<stove_controller>().occupied == false)
                {
                    inCookingArea = true;
                    activeArea = coll;

                    //Remove velocity and place in work zone
                    GO.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    GO.transform.position = coll.transform.GetChild(0).transform.position;
                    //Call stove cooking functions
                    coll.transform.parent.parent.GetComponent<stove_controller>().ObjectEnter(GO);
                    coll.transform.parent.parent.GetComponent<stove_controller>().Cook(GO);
                }

                //Keep object in place
                //GO.transform.position = coll.transform.GetChild(0).transform.position;

                //Alterante way to add heat
                //cookTimeLeft -= 50 * Time.deltaTime;
                break;

            case "Knife":
                
                break;


        }
    }

    void OnTriggerExit(Collider coll)
    {
        switch (coll.tag)
        {
            case "Test":
                inCookingArea = false;
                coll.transform.parent.parent.GetComponent<stove_controller>().ObjectExit(GO);
                //activeArea = null;
                break;

            case "Knife":
                
                break;
        }
    }

    public void AddHeat(float amount)
    {
        heat += amount * Time.deltaTime;
    }

    
}
