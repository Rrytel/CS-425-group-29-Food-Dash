using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmoothing : MonoBehaviour
{
	// the time interval between start and end
	public float smoothing = 1;

	Transform start;
	Transform end;
	float timer;

    // Start is called before the first frame update
    void Start()
    {
		timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
		start = GetComponent <Transform> ();

		if (timer > smoothing)
		{
			end = GetComponent <Transform> ();
			timer = 0;
		}

		GetComponent <Transform> ().position = Vector3.Lerp (start.position, end.position, timer);
		GetComponent <Transform> ().rotation = Quaternion.Euler (Vector3.Lerp (start.rotation.eulerAngles, end.rotation.eulerAngles, timer));

		timer += Time.deltaTime;
    }
}
