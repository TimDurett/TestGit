using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secondLerper : MonoBehaviour
{
    Vector3 pointA = new Vector3(51.63f, -15f, 17.48f);
    Vector3 pointB = new Vector3(51.63f, -15f, -17.48f);
    void Update()
    {
        transform.position = Vector3.Lerp(pointA, pointB, Mathf.PingPong(Time.time, 1.5f));
    }


}
