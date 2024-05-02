using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotfancont : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0,Random.Range(0.5f,-0.5f),0);
    }
}
