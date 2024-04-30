using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class grasscont : MonoBehaviour
{
    public float pos;
    public void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x, Random.Range(0.2f,0.6f), transform.localScale.z);
        pos = transform.localScale.y;
        mowscript.God.MC.grasses.Add(this);
    }

    public void mow()
    {
        pos = 0.1f;
    }

    public void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, pos, transform.localScale.z);
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
