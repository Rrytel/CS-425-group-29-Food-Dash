using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stove_controller : MonoBehaviour
{

    ParticleSystem ps;
    public Image LoadingBar;
    public Image marker;
    public Image CompleteMarker;
    float timerVal;
    float angle;
    float panHeight;
    public bool occupied = false;
    float deltaPan;
    float panDirection = .1f;
    Color lerpedColor = Color.green;
    public Collider cookVolume;
    public List <GameObject> activeItems = null;

    // Start is called before the first frame update
    void Start()
    {
         marker.enabled = false;
         CompleteMarker.enabled = false;
         ps = GetComponent<ParticleSystem>();
         LoadingBar.fillAmount = 0;
         angle = 0;
         timerVal = 0;
         ps.Stop(true);
         panHeight = transform.Find("pan").transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        
        var emission = ps.emission;
        //Testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Spacebar was pressed 
            Debug.Log("Hello: ");
            //emission.enabled = false;
            ps.Stop(true);
            angle += 15;
        }
        //GetComponentInChildren<Transform>().eulerAngles = new Vector3(0f, angle, 0f);


        


    }

    public void Cook(GameObject food)
    {
        //Start cooking process
        Debug.Log("Cook");
        StartCoroutine(cookProcess(food));
    }

    IEnumerator colorShiftCMark(Color start, Color end)
    {
        float timer = 0f;
        bool done = false;
        Color tempColor;
        //Debug.Log("CSM");
        do
        {
            //Set color based on timer
            tempColor = Color.Lerp(start, end, timer);
            CompleteMarker.color = tempColor;
            //Step timer
            if(timer<1)
            {
                timer += Time.deltaTime * .9f;
            }
            
            if (!(timer < 1))
            {
                done = true;
            }
            
            yield return null;
        }while(done!=true);
    }

    IEnumerator cookProcess(GameObject food)
    {
        //Debug.Log("CookP");
        int indicatorStatus = 0;
        occupied = true;
        CompleteMarker.enabled = true;
        marker.enabled = true;
        timerVal = food.GetComponent<test_food_script>().heat;
        //Turn on flame particle
        ps.Play(true);
        do
        {
            //Debug.Log("Cooking");

            //Get and set pan data
            panHeight = transform.Find("pan").transform.position.y;
            if (panHeight > .18f)
            {
                panDirection = -.1f;
            }
            if (panHeight < 0f)
            {
                panDirection = .1f;
            }
            deltaPan = panDirection * Time.deltaTime;

            //Animate pan
            transform.Find("pan").eulerAngles = new Vector3(0f, angle, 0f);
            transform.Find("pan").position += new Vector3(0f, deltaPan, 0f);

            //Update visual timer
            float burn = food.GetComponent<test_food_script>().burnThresh;
            float cook = food.GetComponent<test_food_script>().cookThresh;
            if (timerVal < burn)
            {
                timerVal += 5.0f * Time.deltaTime;
            }
            LoadingBar.fillAmount = timerVal / burn;
            //Adjust color of timer depending on foods cock status
            if((timerVal/burn)<(cook/burn))
            {
                //First half of timer
                //lerpedColor = Color.Lerp(Color.white, Color.green, (timerVal / burn) * 2);
                lerpedColor = Color.Lerp(Color.white, Color.green, (timerVal / cook));
            }
            else if ((timerVal/burn)<1)
            {
                //Second half of timer
                //lerpedColor = Color.Lerp(Color.green, Color.red, ((timerVal / burn) -.5f) * 2);
                lerpedColor = Color.Lerp(Color.green, Color.red, ((timerVal / burn) - (cook/burn))*2);
                //Mark food as done cooking
                //CompleteMarker.color = Color.green;
                if(indicatorStatus == 0)
                {
                    StartCoroutine(colorShiftCMark(Color.white, Color.green));
                    indicatorStatus = 1;
                }
                
            }
            else
            {
                //Mark food as burnt
                //CompleteMarker.color = Color.red;
                if(indicatorStatus == 1)
                {
                    StopCoroutine(colorShiftCMark(Color.white, Color.green));
                    StartCoroutine(colorShiftCMark(Color.green, Color.red));
                    indicatorStatus = 2;
                }
               
            }
            
            LoadingBar.color = lerpedColor;

            //Update pan metrics
            angle += .1f;

            //Cook food
            food.GetComponent<test_food_script>().AddHeat(5.0f);
                     //food.BroadcastMessage("AddHeat", 5.0);


            yield return null;
        } while (activeItems.Contains(food.gameObject));
        //} while (food.GetComponent<test_food_script>().activeArea == cookVolume );
        //Reset values 
        transform.Find("pan").localPosition = new Vector3(0f, 0f, 0f);
        occupied = false;
        marker.enabled = false;
        CompleteMarker.enabled = false;
        CompleteMarker.color = Color.white;
        ps.Stop(true);
        LoadingBar.fillAmount = 0;
        timerVal = 0;
    }

    public void ObjectEnter(GameObject food)
    {
        if(occupied)
        {
            return;
        }
        activeItems.Add(food.gameObject);
    }

    public void ObjectExit(GameObject food)
    {
        if(activeItems.Contains(food.gameObject))
        {
            activeItems.Remove(food.gameObject);
        }
        
    }
}
