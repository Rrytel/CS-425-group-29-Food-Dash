using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stove_controller : MonoBehaviour
{

    ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
         ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var emission = ps.emission;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Spacebar was pressed 
            Debug.Log("Hello: ");
            //emission.enabled = false;
            ps.Stop(true);
        }
    }
}
