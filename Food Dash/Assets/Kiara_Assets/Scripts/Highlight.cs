using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//credit: https://answers.unity.com/questions/232180/best-way-to-highlight-an-object-on-mouse-over.html
public class Highlight : MonoBehaviour
{
    private Color originalColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //originalColor = renderer.material.color;
        //renderer.material.color = Color.white;
    }

    void OnTriggerExit(Collider other)
    {
        //renderer.material.color = originalColor;
    }
}
