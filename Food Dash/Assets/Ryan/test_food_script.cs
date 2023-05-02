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
	public bool chopped = false;
    
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
    public float throwPow = 1.5f;
    bool chargeUp = false;

    public TrailRenderer TR;
    public Material cookedMat;
    public Material burntMat;
    public Mesh choppedMesh;
    public Material choppedMat;
    public bool isChoppable;
    public bool isCookable;
    public ParticleSystem chopParticle;
    float unGrabTimer = 0f;

    void Start()
    {
        
        meshF = GO.GetComponent<MeshFilter>();
        m_Rigidbody = GetComponent<Rigidbody>();
        //gameObject.GetComponent<MeshRenderer>().material = Material1;
        //meshF.sharedMesh = mesh1;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isHeld)
        {
            unGrabTimer += 5 * Time.deltaTime;
        }

        //End trail early
        if(!isHeld && m_Rigidbody.velocity.magnitude < 1 && unGrabTimer > 0.1f)
        {
            //throwPow -= 1 * Time.deltaTime;
		    if(TR!=null)
		    {
			    //TR.enabled = false;
		    }
            
            //throwPow = 1.5f;
        }


        //Spin charge
        if(chargeUp)
        {
            if(throwPow < 10)
            {
                //Scale up throwing power
                throwPow += 3f * Time.fixedDeltaTime;
            }
            //Remove controller bound rotation
            gameObject.GetComponent<XRGrabInteractable>().trackRotation = false;
            transform.Rotate(new Vector3(25, 12, 34));
            
        }
        else
        {
            gameObject.GetComponent<XRGrabInteractable>().trackRotation = true;
        }
        gameObject.GetComponent<XRGrabInteractable>().throwVelocityScale = throwPow;


	    if(TR!=null)
	    {
		    if(throwPow>5)
        	{
            	TR.enabled = true;
        	}
        	else
        	{
            	TR.enabled = false;
        	}
	    }



        //timer += .01f;
        //Update mesh based on stage in cooking process
        if (heat > cookThresh && cooked == false)
        {
            //Update variables
            cooked = true;
            //Swap for cooked mesh and material

            gameObject.GetComponent<MeshRenderer>().material = cookedMat;
            //meshF.sharedMesh = Resources.Load<Mesh>("heat_lamp");



            //Play sound for cooked food
            audioSource.PlayOneShot(cookedSound, .3f);
        }
        if(heat > burnThresh && burnt == false)
        {
            //Update variables
            burnt = true;
            //Swap for burnt mesh and material

            gameObject.GetComponent<MeshRenderer>().material = burntMat;
            //meshF.sharedMesh = Resources.Load<Mesh>("sink_handwash"); 
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
			chopped = true;
            //Swap for chopped mesh
            //meshF.sharedMesh = Resources.Load<Mesh>("sink_handwash");
            meshF.sharedMesh = choppedMesh;
            gameObject.GetComponent<MeshRenderer>().material = choppedMat;
            

        }
    }

    void OnTriggerEnter(Collider coll)
    {
      
        //activeArea = coll;
        switch (coll.tag)
        {
            case "Test":
                if (!isCookable)
                    return;
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

                        coll.transform.parent.parent.GetComponent<stove_controller>().ObjectExit(tempFood.gameObject);
                        //coll.transform.parent.parent.GetComponent<stove_controller>().ResetCook();

                        coll.transform.parent.parent.GetComponent<stove_controller>().EjectFood(tempFood, throwPow);
                        coll.transform.parent.parent.GetComponent<stove_controller>().ResetCook() ;
                        //Remove velocity and place in work zone
                        GO.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        GO.transform.position = coll.transform.GetChild(0).transform.position;
                        coll.transform.parent.parent.GetComponent<stove_controller>().ObjectEnter(GO);



                        //coll.transform.parent.parent.GetComponent<stove_controller>().ObjectExit(tempFood.gameObject);
                        //tempFood.gameObject.GetComponent<Rigidbody>().MovePosition(coll.transform.parent.parent.GetComponent<stove_controller>().launchPos.transform.position);
                        //tempFood.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 500, -60));

                        //coll.transform.parent.parent.GetComponent<stove_controller>().ObjectExit(tempFood.gameObject);
                        //coll.transform.parent.parent.GetComponent<stove_controller>().activeItems.Clear();
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
                
                if(isChoppable)
                {
                    //Increment chop counter
                    chopState += 1;
                    //Play chop sound

                    //Play chop animation
                    chopParticle.Emit(1);
                }


                break;

            case "Rat zone":
                //Rats will not be bumped
                if(GO.tag == "Rat")
                {
                    break;
                }
                //Player held objects should not be bumped
                if(isHeld)
                {
                    break;
                }
                throwPow = 10;
                //Get forward direction of acting object and apply magnitude transformations
                Vector3 forward = coll.transform.parent.transform.forward * 90 * (1 / 1.5f);
                //Get vertical component and combine
                Vector3 vertical = new Vector3(0f, 500f, 0f);
                Vector3 launchVec = forward + vertical;
                //Bump object
                GO.GetComponent<Rigidbody>().AddForce(launchVec);
                GO.GetComponent<Rigidbody>().AddRelativeTorque(5, 5, 5);
                break;
        
        
        }
    }

    void OnTriggerStay(Collider coll)
    {
        switch (coll.tag)
        {
            case "Test":
                if (!isCookable)
                    return;
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
        unGrabTimer = 0f;
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
            timer += 1f;
            yield return null;
        } while (timer < 2f);
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
