using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class rat_script : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public NavMeshAgent agent;
    public Transform goal;
    Mesh mesh;
    private Renderer mr;
    private bool isHeld = false;
    private bool inVaccum = false;
    private bool touchFloor = false;

    // Start is called before the first frame update
    void Start()
    {
        //Find coresponding components
        m_Rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        mesh = GetComponent<MeshFilter>().mesh;
        mr = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(agent.hasPath)
        {
            //Rat notices food, anime eyes
            mr.material.color = Color.red;


        }
        
        //Set goal
        if(agent.enabled)
        {
            agent.destination = goal.position;
        }
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        switch(coll.tag)
        {
            case "vaccum":
                m_Rigidbody.drag = 1;
                inVaccum = true;
                break;
        }
    }

    void OnTriggerStay(Collider coll)
    {
        switch (coll.tag)
        {
            case "vaccum":
                if (this.gameObject.GetComponent<test_food_script>().active == true)
                {
                    break;
                }
                //Transform destinationTransform = coll.GetComponentInParent<Transform>();
                Transform destinationTransform = coll.transform.parent.GetComponent<Transform>();
                Vector3 moveDir = destinationTransform.position - m_Rigidbody.transform.position;
                //m_Rigidbody.MovePosition(m_Rigidbody.position + moveDir * Time.fixedDeltaTime);
                m_Rigidbody.AddForce(moveDir * 10000 * Time.fixedDeltaTime);
                agent.enabled = false;
                break;
        }
    }

    void OnTriggerExit(Collider coll)
    {
        switch (coll.tag)
        {
            case "vaccum":
                inVaccum = false;
                StartCoroutine(wakeUp());
                break;

        }
    }

    IEnumerator wakeUp()
    {
        //Time to wake up after being interacted with
        float breakOutTimer = 0f;
        float floorTimer = 0f;
        
        do
        {
            //Wait to see if entity is picked up or is in range of vaccum
            breakOutTimer += 1f * Time.fixedDeltaTime;
            //Debug.Log(breakOutTimer);
            if(touchFloor)
            {
                //If floor is being touched increment floor timer for faster wake up
                floorTimer += 1f * Time.fixedDeltaTime;
            }
            yield return null;
        } while (!isHeld && !inVaccum && floorTimer < 5f && breakOutTimer < 20f);

        if (breakOutTimer > 19f || floorTimer > 4f)
        {
            //Wake up if not interacted with for a time threshold
            agent.enabled = true;
            m_Rigidbody.drag = 10;
            Debug.Log("wakeup");
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.CompareTag("floor"))
        {
            touchFloor = true;
            //Debug.Log("floor");
        }
    }

    private void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.CompareTag("floor"))
        {
            touchFloor = false;
            //Debug.Log("floor");
        }
    }

    public void grab()
    {
        agent.enabled = false;
        isHeld = true;
        //m_Rigidbody.drag = 1;
    }
    public void unGrab()
    {
        m_Rigidbody.drag = 1;
        isHeld = false;
        StartCoroutine(wakeUp());
    }
}
