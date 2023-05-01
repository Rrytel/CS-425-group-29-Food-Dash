using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHoleController_scr : MonoBehaviour
{
    public bool active = false;
    public ParticleSystem ps;

    private void Start()
    {
        ps.Stop();
        active = false;
    }

    private void Update()
    {
        
    }

    public void grab()
    {
        active = true;
        ps.Play();
    }
    public void unGrab()
    {
        active = false;
        ps.Stop();
    }
}
