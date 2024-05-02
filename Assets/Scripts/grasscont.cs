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
        StartCoroutine(Growback());
    }

    public void mow()
    {
        StopCoroutine(Growback());
        pos = 0.1f;
        StartCoroutine(Growback());
    }

    public void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, pos, transform.localScale.z);
    }
    
    public IEnumerator Growback()
    {
        Debug.Log("called");
        while (pos <= 2)
        {
           pos += Random.Range(0.0005f, 0.001f);
           yield return new WaitForSeconds(Random.Range(1,4));
        }
    }
    
}
