using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bait_controller : MonoBehaviour
{
    public List<GameObject> baitList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBait(GameObject bait)
    {
        
         baitList.Add(bait);
        

    }
    public void RemoveBait(GameObject bait)
    {
        
        
         baitList.Remove(bait);
        

    }
}
