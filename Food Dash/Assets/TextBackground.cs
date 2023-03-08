using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class TextBackground : MonoBehaviour
{
	public GameObject parentText;

	RectTransform parentTransform;
	RectTransform backgroundTransform;
    // Start is called before the first frame update
    void Start ()
    {
		parentTransform = parentText.GetComponent <RectTransform> ();
    }

    // Update is called once per frame
    void Update()
    {
		backgroundTransform = parentTransform;
    }
}
