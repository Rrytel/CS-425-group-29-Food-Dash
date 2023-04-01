using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class player_move : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody m_rig;
    void Start()
    {
        m_rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var translation = UnityEngine.XR.CommonUsages.primary2DAxis;

        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            Debug.Log(string.Format("Device found with name '{0}' and role '{1}'", device.name, device.role.ToString()));

        }
        
        Vector2 input;
        inputDevices[1].TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out input);
        //Debug.Log(translation);
        //translation.As();
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, .28f, gameObject.transform.position.z);
        //float rotation = Input.GetAxis("Horizontal");
        Vector3 moveDirection = new Vector3(input.x, 0f, input.y);
        Debug.Log(moveDirection);
        //gameObject.GetComponent<CharacterController>().Move(moveDirection * Time.deltaTime);
        //m_rig.velocity = (moveDirection * 10f);
        m_rig.AddForce(moveDirection * 10f);
    }
}
