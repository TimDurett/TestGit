using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lerper : MonoBehaviour
{
    Vector3 pointA = new Vector3(8.51f, 1, 0);
    Vector3 pointB = new Vector3(-8.51f, 1, 0);
    void Update()
    {
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time, 2));
    }




}
