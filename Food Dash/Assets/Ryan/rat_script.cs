using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class rat_script : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public NavMeshAgent agent;
    public Transform goal;
    public GameObject baitGoal;
    Mesh mesh;
    public List <GameObject> baitList;
    private Renderer mr;
    private bool isHeld = false;
    private bool inVaccum = false;
    private bool touchFloor = false;
    private bool seekingBait = false;
    public float baitRaidus = 5;

    public float wanderRad = 500f;

    private float wanderTimer = 0f;

    GameObject baitController;


    // Start is called before the first frame update
    void Start()
    {
        //Find coresponding components
        m_Rigidbody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        mesh = GetComponent<MeshFilter>().mesh;
        mr = GetComponent<Renderer>();
        baitController = GameObject.FindGameObjectWithTag("BaitController");
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
        if (!agent.enabled)
        {
            gameObject.GetComponent<SingularityPullable>().pullable = true;
            return;
        }
        gameObject.GetComponent<SingularityPullable>().pullable = false ;

        //agent.destination = new Vector3(.409f, .598f, -0.425f);
        //return;


        //Bait
        //

        seekingBait = false;
        baitGoal = FindClosestBait();
        if(baitGoal != null)
        {
            if ((baitGoal.transform.position - transform.position).sqrMagnitude < baitRaidus)
            {
                agent.destination = baitGoal.transform.position;
                seekingBait = true;
            }
        }
        
        
        //agent.destination = goal.position;







        //Wandering functionality
        if(seekingBait)
        {
            return;
        }

        //Debug.Log(wanderTimer);
        if(!(wanderTimer > 10))
        {
            wanderTimer += 1 * Time.fixedDeltaTime;
            return;
        }
        //Reset wander timer
        wanderTimer = 0;
        //Get random position to wander to
        Vector3 tempPos = RandomNavSphere(transform.position, wanderRad, -1);
        //Debug.Log(tempPos);
        agent.destination = tempPos;
        
    }

    GameObject FindClosestBait()
    {
        GameObject closest = null;
        Vector3 diff;
        float curDist;
        float minDist = Mathf.Infinity;
        
        if(baitController.GetComponent<bait_controller>().baitList.Count == 0)
        {
            return null;
        }
        foreach (GameObject bait in baitController.GetComponent<bait_controller>().baitList)
        {
            diff = gameObject.transform.position - bait.transform.position;
            curDist = diff.sqrMagnitude;
            if(curDist<minDist)
            {
                minDist = curDist;
                closest = bait;
            }
        }
        return closest;
    }

    public void AddBait(GameObject bait)
    {
        if(!(baitList.Contains(bait)) )
        {
            baitList.Add(bait);
        }
        
    }
    public void RemoveBait(GameObject bait)
    {
        if(baitList.Contains(bait))
        {
            baitList.Remove(bait);
        }
        
    }

    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        //Get Random point in a 5f radius sphere
        Vector3 randomDirection = Random.insideUnitSphere * 5f;
        //Center sphere on current body
        randomDirection += origin;

        NavMeshHit navHit;
        //Remove vertical component
        //randomDirection.y = 0;
        NavMesh.SamplePosition(randomDirection, out navHit, 10, layermask);
        return navHit.position;
    }

    private void OnTriggerEnter(Collider coll)
    {
        switch(coll.tag)
        {
            case "vaccum":
                m_Rigidbody.drag = 1;
                inVaccum = true;
                break;
            case "black hole":
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

                //Get location of vaccum
                Transform destinationTransform = coll.transform.parent.GetComponent<Transform>();
                Vector3 moveDir = destinationTransform.position - m_Rigidbody.transform.position;
                //m_Rigidbody.MovePosition(m_Rigidbody.position + moveDir * Time.fixedDeltaTime);

                //Suction
                m_Rigidbody.AddForce(moveDir * 10000 * Time.fixedDeltaTime);
                agent.enabled = false;
                break;

            case "black hole":
                if(coll.gameObject.GetComponent<blackHoleController_scr>().active == true)
                {
                    
                    agent.enabled = false;
                }
                if (coll.gameObject.GetComponent<blackHoleController_scr>().active == false)
                {
                    agent.enabled = true;
                }
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

            case "black hole":
                inVaccum = false;
                StartCoroutine(wakeUp());
                //agent.enabled = true;
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
            if(!isHeld && !inVaccum)
            {
                //Wake up if not interacted with for a time threshold
                agent.enabled = true;
                m_Rigidbody.drag = 10;
                Debug.Log("wakeup");
            }
            
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.CompareTag("floor"))
        {
            touchFloor = true;
        }
    }

    private void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.CompareTag("floor"))
        {
            touchFloor = false;
        }
    }

    public void grab()
    {
        agent.enabled = false;
        isHeld = true;
        m_Rigidbody.drag = 1;
    }
    public void unGrab()
    {
        m_Rigidbody.drag = 1;
        isHeld = false;
        StartCoroutine(wakeUp());
    }
}
