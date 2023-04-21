using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//credit : https://www.w3schools.blog/unity-get-speed-of-object

public class ratAnimatorScript : MonoBehaviour
{
    public Vector3 oldPos;
    public float speed;
    public Animator m_Animator;
    bool canWalk = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //check if the object's speed is > .1 and if so setTrigger to walk
        speed = Vector3.Distance(oldPos, transform.position);
        oldPos = transform.position;

        if (canWalk && speed > .01)
        {
            m_Animator.SetTrigger("Walk");
        }
        else if (!canWalk)
        {
            m_Animator.SetTrigger("Idle");
        }

        //Testing animation
        /*	
            If (!canWalk)
            {
                m_Animator.SetTrigger(“Walk”);
            }else if(canWalk && speed > .01){

                m_Animator.SetTrigger(“Idle”);

            }
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        canWalk = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        canWalk = true;
    }
}


