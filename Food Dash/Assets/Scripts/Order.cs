using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{   
    // food or drinks in the order
    List <FoodTypes> items;
    bool ready = false;

    // Start is called before the first frame update
    void Start()
    {
        CreateOrder ();
    }

    // Update is called once per frame
    void Update()
    {
        if (items.Count == 0)
        {
            ready = true;
        }
    }

    void CreateOrder ()
    {
        for (int iterations = 0; iterations <= Random.Range (1, 3); iterations += 1)
        {
            items.Add (FoodTypes.Burger);
        }
    }
}
