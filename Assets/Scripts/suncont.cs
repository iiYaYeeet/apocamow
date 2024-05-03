using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suncont : MonoBehaviour
{
    public float rotspeed;
    void Update()
    {
        transform.Rotate(rotspeed,0,0);
    }
}
