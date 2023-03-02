using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class unit_tests : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        test_DegreeToWorldDir_gives_correct_direction_typical();
        test_DegreeToWorldDir_gives_correct_direction_edge_zero();
        test_DegreeToWorldDir_gives_correct_direction_edge_three_sixty();
    }

    Vector3 DegreeToWorldDir(int deg)
    {
        if(deg >= 360)
        {
            deg -= 360;
        }
        Vector3 dirVec;
        float rad = deg * Mathf.Deg2Rad;
        float x =  Mathf.Cos(rad);
        float z =  Mathf.Sin(rad);
        dirVec = new Vector3(x, 0, z);
        return dirVec;

    }

    void test_DegreeToWorldDir_gives_correct_direction_typical()
    {
        MethodBase methodBase = MethodBase.GetCurrentMethod();
        Vector3 testVec = DegreeToWorldDir(30);
        if(testVec.x == (Mathf.Sqrt(3)/2) && testVec.y == 0f && testVec.z == (1f/2f))
        {
            Debug.Log("test passed: " + methodBase.Name);
        }
    }

    void test_DegreeToWorldDir_gives_correct_direction_edge_zero()
    {
        MethodBase methodBase = MethodBase.GetCurrentMethod();
        Vector3 testVec = DegreeToWorldDir(0);
        if (testVec.x == 1f && testVec.y == 0f && testVec.z == 0f)
        {
            Debug.Log("test passed: " + methodBase.Name);
        }
    }

    void test_DegreeToWorldDir_gives_correct_direction_edge_three_sixty()
    {
        MethodBase methodBase = MethodBase.GetCurrentMethod();
        Vector3 testVec = DegreeToWorldDir(360);
        if (testVec.x == 1f && testVec.y == 0f && testVec.z == 0f)
        {
            Debug.Log("test passed: " + methodBase.Name);
        }
    }
}
