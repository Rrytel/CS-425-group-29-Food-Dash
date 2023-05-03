using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//credit: https://answers.unity.com/questions/232180/best-way-to-highlight-an-object-on-mouse-over.html

//credit: https://docs.unity3d.com/ScriptReference/Color-clear.html

public class Highlight : MonoBehaviour
{
    private Color originalColor;
    Renderer m_render;
    // Start is called before the first frame update
    void Start()
    {
        //get the renderer of the game object
        m_render = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //originalColor = renderer.material.color;
        //renderer.material.color = Color.white;
        m_render.material.color = Color.white;

    }

    void OnTriggerExit(Collider other)
    {
        m_render.material.color = Color.clear;
    }
}
