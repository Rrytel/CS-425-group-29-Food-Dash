using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stove_controller : MonoBehaviour
{

    ParticleSystem ps;
    public Image LoadingBar;
    public Image marker;
    float timerVal;
    float angle;
    float panHeight;
    public bool occupied = false;
    float deltaPan;
    float panDirection = .1f;
    Color lerpedColor = Color.green;
    // Start is called before the first frame update
    void Start()
    {
         marker.enabled = false;
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

    IEnumerator cookProcess(GameObject food)
    {
        occupied = true;
        marker.enabled = true;
        //Turn on flame particle
        ps.Play(true);
        do
        {
            Debug.Log("Cooking");

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
            if (timerVal < burn)
            {
                timerVal += 5.0f * Time.deltaTime;
            }
            LoadingBar.fillAmount = timerVal / burn;
            lerpedColor = Color.Lerp(Color.green, Color.red, (timerVal/burn));
            LoadingBar.color = lerpedColor;

            //Update pan metrics
            angle += .1f;

            //Cook food
            food.GetComponent<test_food_script>().AddHeat(5.0f);
                     //food.BroadcastMessage("AddHeat", 5.0);


            yield return null;
        } while (food.GetComponent<test_food_script>().inCookingArea);
        //Reset values 
        transform.Find("pan").position = new Vector3(0f, 0f, 0f);
        occupied = false;
        marker.enabled = false;
        ps.Stop(true);
        LoadingBar.fillAmount = 0;
    }
}
