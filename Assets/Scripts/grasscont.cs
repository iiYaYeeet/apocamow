using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class grasscont : MonoBehaviour
{
    public float pos;
    public void Start()
    {
        mowscript.God.MC.grasses.Add(this);
    }

    public void mow()
    {
        pos = -2.5f;
    }

    public void Update()
    {
        transform.position = new Vector3(transform.position.x, pos, transform.position.z);
    }
    
    public IEnumerator Growback()
    {
        while (pos > -0.05f)
        {
            Mathf.Lerp(pos, 0, 1);
            yield return new WaitForSeconds(3);
        }
    }
    
}
