using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_way_scr : MonoBehaviour
{
    public bool isOpening = false;
    public bool isClosing = false;
    public bool isOpen = false;
    private float rightDoorPos = 0;
    private float leftDoorPos = 0;
    private float closeTimer = 10;
    private int playerCount = 0;
    public Transform LDoor;
    public Transform RDoor;
    Coroutine CurrentCoroutine;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(isOpen && playerCount == 0)
            closeTimer -= 5 * Time.deltaTime;
        if(closeTimer < 0)
        {
            
            if (!isOpen)
                return;
            if (isClosing)
                return;
            //if (CurrentCoroutine != null)
            //return;
            
            isClosing = true;
            isOpen = false;
            CurrentCoroutine = StartCoroutine(SwingClose());
            
        }
    }

    public void AddEntity()
    {
        playerCount += 1;
    }

    public void RemoveEntity()
    {
        playerCount -= 1;
    }

    public void OpenFromNorth()
    {
        Debug.Log("North door");
        //Do nothing if door is busy
        if (isOpening)
            return;
        if (isOpen)
            return;

        //Check if door is closing
        if(isClosing)
        {
            Debug.Log("Stop");
            StopCoroutine(CurrentCoroutine);
            isClosing = false;
            CurrentCoroutine = null;
        }
        //Mark door as busy
        isOpening = true;
        CurrentCoroutine = StartCoroutine(SwingNorth());
    }

    public void OpenFromSouth()
    {
        Debug.Log("South door");
        //Do nothing if door is busy
        if (isOpening)
            return;
        if (isOpen)
            return;
        //Check if door is closing
        if (isClosing)
        {
            Debug.Log("Stop");
            StopCoroutine(CurrentCoroutine);
            isClosing = false;
            CurrentCoroutine = null;
        }
        //Mark door as busy
        isOpening = true;
        CurrentCoroutine = StartCoroutine(SwingSouth());
    }

    IEnumerator SwingClose()
    {
        //Animate swinging closed

        //Initalize data
        //leftDoorPos = LDoor.rotation.y;
        //rightDoorPos = RDoor.rotation.y;
        
        int direction = 1;

        //Find direction
        if (leftDoorPos < 0)
            direction = -1;
        bool done = false;
        do
        {
            Debug.Log("Swing Close");
            //Debug.Log(leftDoorPos);
            //Increment and set new door positions
            leftDoorPos -= 35 * direction * Time.deltaTime;
            LDoor.rotation = Quaternion.Euler(LDoor.rotation.x, leftDoorPos, LDoor.rotation.z);

            rightDoorPos += 35 * direction * Time.deltaTime;
            RDoor.rotation = Quaternion.Euler(RDoor.rotation.x, rightDoorPos, RDoor.rotation.z);

            //Check door pos for complete swing
            if (rightDoorPos <5 && rightDoorPos >-5)
                done = true;
            yield return null;
        } while (done != true);
        //Set markers
        CurrentCoroutine = null;
        isClosing = false;
        isOpen = false;
    }

    IEnumerator SwingNorth()
    {
        //Animate swinging north

        //Initalize data
        //leftDoorPos = LDoor.rotation.y;
        //rightDoorPos = RDoor.rotation.y;
        bool done = false;
        do
        {
            Debug.Log("SwingNorth");
            //Increment and set new door positions
            leftDoorPos -= 35 * Time.deltaTime;
            LDoor.rotation = Quaternion.Euler(LDoor.rotation.x, leftDoorPos, LDoor.rotation.z);

            rightDoorPos += 35 * Time.deltaTime;
            RDoor.rotation = Quaternion.Euler(RDoor.rotation.x, rightDoorPos, RDoor.rotation.z);

            //Check door pos for complete swing
            if (rightDoorPos > 120)
                done = true;
            yield return null;
        } while (done != true);
        //Set markers
        CurrentCoroutine = null;
        isOpening = false;
        isOpen = true;
        closeTimer = 10f;

    }

    IEnumerator SwingSouth()
    {
        //Animate swinging north
        //leftDoorPos = LDoor.rotation.y;
        //rightDoorPos = RDoor.rotation.y;
        bool done = false;
        do
        {
            Debug.Log("SwingSouth");
            //Increment and set new door positions
            leftDoorPos += 35 * Time.deltaTime;
            LDoor.rotation = Quaternion.Euler(LDoor.rotation.x, leftDoorPos, LDoor.rotation.z);

            rightDoorPos -= 35 * Time.deltaTime;
            RDoor.rotation = Quaternion.Euler(RDoor.rotation.x, rightDoorPos, RDoor.rotation.z);

            //Check door pos for complete swing
            if (rightDoorPos < -120)
                done = true;
            yield return null;
        } while (done != true);
        //Set markers
        CurrentCoroutine = null;
        isOpening = false;
        isOpen = true;
        closeTimer = 10f;
    }
}
