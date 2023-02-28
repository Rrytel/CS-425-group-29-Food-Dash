using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knife_script : MonoBehaviour
{
    private bool isHeld = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {

        //activeArea = coll;
        switch (coll.tag)
        {
            case "Rat zone":
                //Player held objects should not be bumped
                if(isHeld)
                {
                    break;
                }
                //Get forward direction of acting object and apply magnitude transformations
                Vector3 forward = coll.transform.parent.transform.forward * 90 * (1 / 1.5f);
                //Get vertical component and combine
                Vector3 vertical = new Vector3(0f, 500f, 0f);
                Vector3 launchVec = forward + vertical;
                //Bump object
                gameObject.GetComponent<Rigidbody>().AddForce(launchVec);
                gameObject.GetComponent<Rigidbody>().AddRelativeTorque(5, 5, 5);
                break;
        }
    }

    public void grab()
    {
        isHeld = true;
    }
    public void unGrab()
    {
        isHeld = false;
    }
}
