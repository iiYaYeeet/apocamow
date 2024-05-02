using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fancont : MonoBehaviour
{
    public float rotspeed;
    void Update()
    {
        transform.Rotate(0,rotspeed,0);
    }
}
