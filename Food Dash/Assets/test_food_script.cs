using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class test_food_script : MonoBehaviour
{
    // Start is called before the first frame update
    string target;
    Collider tempCol;
    public bool isHeld = false;
    public float cookTimeLeft = 100;
    public GameObject GO;
    //Food attributes
    public string foodType;
    public bool cooked = false;
    public bool burnt = false;
    
    public float heat = 0;
    public float cookThresh;
    public float burnThresh;
    public int chopThresh;
    public AudioSource audioSource;
    public AudioClip cookedSound;
    public AudioClip burntSound;
    MeshFilter meshF;
    public Collider activeArea;
    public List<Collider> activeAreas = null;
    public List<Collider> prevAreas = null;
    Rigidbody m_Rigidbody;
    int chopState = 0;
    public bool active = false;
    float throwPow = 1.5f;
    bool chargeUp = false;
    

    
    void Start()
    {
        
        meshF = GO.GetComponent<MeshFilter>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Spin charge
        if(chargeUp)
        {
            if(throwPow < 10)
            {
                //Scale up throwing power
                throwPow += 1f * Time.fixedDeltaTime;
            }
            //Remove controller bound rotation
            gameObject.GetComponent<XRGrabInteractable>().trackRotation = false;
            transform.Rotate(new Vector3(25, 12, 34));
            gameObject.GetComponent<XRGrabInteractable>().throwVelocityScale = throwPow;
        }
        else
        {
            gameObject.GetComponent<XRGrabInteractable>().trackRotation = true;
        }

        //timer += .01f;
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

            //Kill Rat
            if(GO.GetComponent<rat_script>())
            {
                //GO.GetComponent<rat_script>().enabled = false;
                GO.GetComponent<Rigidbody>().drag = 1;
                Destroy(GO.GetComponent<rat_script>());
            }
            

        }

        //Update mesh based on stage in chopping process
        if(chopState > chopThresh)
        {
            //Update variables

            //Swap for chopped mesh
            meshF.sharedMesh = Resources.Load<Mesh>("sink_handwash");
        }
    }

    void OnTriggerEnter(Collider coll)
    {
      
        //activeArea = coll;
        switch (coll.tag)
        {
            case "Test":
                activeAreas.Add(coll);
                if(isHeld)
                {

                }
                if(isHeld == false)
                {
                    if(prevAreas.Contains(coll))
                    {
                        break;
                    }
                    if(active)
                    {
                        break;
                    }
                    //Kick food out
                    if (coll.transform.parent.parent.GetComponent<stove_controller>().occupied == true)
                    {
                        GameObject tempFood = coll.transform.parent.parent.GetComponent<stove_controller>().activeItems[0];
                        //GO.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        coll.transform.parent.parent.GetComponent<stove_controller>().ObjectExit(tempFood.gameObject);
                        coll.transform.parent.parent.GetComponent<stove_controller>().ResetCook();
                        
                        coll.transform.parent.parent.GetComponent<stove_controller>().EjectFood(tempFood, throwPow);
                        //coll.transform.parent.parent.GetComponent<stove_controller>().ObjectEnter(GO);


                        //coll.transform.parent.parent.GetComponent<stove_controller>().ObjectExit(tempFood.gameObject);
                        //tempFood.gameObject.GetComponent<Rigidbody>().MovePosition(coll.transform.parent.parent.GetComponent<stove_controller>().launchPos.transform.position);
                        //tempFood.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, -60));

                        //coll.transform.parent.parent.GetComponent<stove_controller>().activeItems.Remove(tempFood.gameObject);
                        //tempFood.GetComponent<test_food_script>().DA();
                    }


                }
                /*if (coll.transform.parent.parent.GetComponent<stove_controller>().occupied == false)
                {
                   
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
                //Increment chop counter
                chopState += 1;
                //Play chop sound

                break;
        
        
        }
    }

    void OnTriggerStay(Collider coll)
    {
        switch (coll.tag)
        {
            case "Test":
                activeArea = coll;
                //Check to see if stove is busy
                if (coll.transform.parent.parent.GetComponent<stove_controller>().occupied == false && active == false)
                {
                    active = true;
                    
                    //activeArea = coll; // BAD!

                    if(!isHeld)
                    {
                        //Remove velocity and place in work zone
                        GO.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        GO.transform.position = coll.transform.GetChild(0).transform.position;
                    }
                    
                    //Call stove cooking functions
                    coll.transform.parent.parent.GetComponent<stove_controller>().ObjectEnter(GO);
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
                activeAreas.Remove(coll);
                activeArea = null;
                prevAreas.Add(coll);
                StartCoroutine(ExitProcess(coll));
                //coll.transform.parent.parent.GetComponent<stove_controller>().ObjectExit(GO);
                break;

            case "Knife":
                
                break;
        }
    }

    

    IEnumerator ExitProcess(Collider coll)
    {
        float timer = 0f;
        do
        {
            //Wait to see if object re-enters trigger zone within a time treshold
            timer += 1f;
            yield return null;
        } while (!activeAreas.Contains(coll) && timer<2f);
        //while (coll != activeArea && timer < 2f);
        if(timer > 1f)
        {
            //Object can exit
            coll.transform.parent.parent.GetComponent<stove_controller>().ObjectExit(GO);
        }
        prevAreas.Remove(coll);
    }

    public void AddHeat(float amount)
    {
        heat += amount * Time.deltaTime;
    }

    public void grab()
    {
        isHeld = true;
        throwPow = 1.5f;
    }
    public void unGrab()
    {
        isHeld = false;
        EndCharge();
    }

    public void DA()
    {
        StartCoroutine(DelayActivate());
    }

    IEnumerator DelayActivate()
    {
        float timer = 0f;
        do
        {
            timer += 1f * Time.fixedDeltaTime;
            yield return null;
        } while (timer < .2f);
        active = false;
    }

    public void StartCharge()
    {
        chargeUp = true;
    }

    public void EndCharge()
    {
        chargeUp = false;
    }

    
}