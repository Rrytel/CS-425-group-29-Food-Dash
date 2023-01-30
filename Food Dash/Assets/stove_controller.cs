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
    public AudioSource audioSource;
    public AudioClip ignitionSound;
    float timerVal;
    float angle;
    float panHeight;
    public bool occupied = false;
    float deltaPan;
    float panDirection = .1f;
    Color lerpedColor = Color.green;
    public Collider cookVolume;
    public List <GameObject> activeItems = null;
    public GameObject launchPos;
    public GameObject smoke;

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
        //Initialize variables
        int indicatorStatus = 0;
        occupied = true;
        CompleteMarker.enabled = true;
        marker.enabled = true;
        timerVal = food.GetComponent<test_food_script>().heat;

        //Turn on flame particle and sounds
        //smoke.GetComponent<ParticleSystem>().Play();
        audioSource.PlayOneShot(ignitionSound, 2f);
        audioSource.Play();
        ps.Play(true);
        do
        {
            //Get and set pan data
            panHeight = transform.Find("pan").transform.position.y;
            if (panHeight > .18f)
            {
                //Lower pad
                panDirection = -.1f;
            }
            if (panHeight < 0f)
            {
                //Raise pan
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
                if(indicatorStatus == 0)
                {
                    StartCoroutine(colorShiftCMark(Color.white, Color.green));
                    indicatorStatus = 1;
                }
            }
            else
            {
                //Mark food as burnt
                if(indicatorStatus == 1)
                {
                    StopCoroutine(colorShiftCMark(Color.white, Color.green));
                    StartCoroutine(colorShiftCMark(Color.green, Color.red));
                    lerpedColor = Color.Lerp(Color.green, Color.red, ((timerVal / burn) - (cook / burn)) * 2);
                    indicatorStatus = 2;
                }
               
            }
            //Set loading bar color
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
        audioSource.Stop();
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
        //Check if stove is currently occupied
        if(occupied)
        {
            return;
        }
        activeItems.Add(food.gameObject);
        Cook(food);
    }

    public void ObjectExit(GameObject food)
    {
        //Check if the food object is in this stoves list of active items
        if(activeItems.Contains(food.gameObject))
        {
            activeItems.Remove(food.gameObject);
            //food.GetComponent<test_food_script>().active = false;
            food.GetComponent<test_food_script>().DA();
        }
    }

    public void EjectFood(GameObject food, float launchPow)
    {
        //smoke.GetComponent<ParticleSystem>().Play();
        //Place active food in launch position
        food.GetComponent<Rigidbody>().MovePosition(launchPos.transform.position);
        //Get forward direction and apply magnitude
        Vector3 forward = transform.forward * 90 * (launchPow/1.5f);
        //Get vertical component and combine
        Vector3 vertical = new Vector3(0f, 500f, 0f);
        Vector3 launchVec = forward + vertical;
        //Launch food
        food.GetComponent<Rigidbody>().AddForce(launchVec);

        //Debug.Log(launchVec);
    }

    public void ResetCook()
    {
        gameObject.GetComponent<stove_controller>().StopAllCoroutines();
        //Reset values 
        audioSource.Stop();
        transform.Find("pan").localPosition = new Vector3(0f, 0f, 0f);
        occupied = false;
        marker.enabled = false;
        CompleteMarker.enabled = false;
        CompleteMarker.color = Color.white;
        ps.Stop(true);
        LoadingBar.fillAmount = 0;
        timerVal = 0;
    }
}
