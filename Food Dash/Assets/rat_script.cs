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
    private bool held = false;
    private bool inVaccum = false;

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
                inVaccum = true;
                break;
        }
    }

    void OnTriggerStay(Collider coll)
    {
        switch (coll.tag)
        {
            case "vaccum":
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
        float timer = 0f;
        do
        {
            //Wait to see if entity is picked up or is in range of vaccum
            timer += 1f * Time.fixedDeltaTime;
            Debug.Log(timer);
            yield return null;
        } while (!held && !inVaccum && timer < 20f);

        if (timer > 19f)
        {
            //Wake up if not interacted with for a time threshold
            agent.enabled = true;
        }
    }
}
