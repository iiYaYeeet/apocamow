using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class grasscont : MonoBehaviour
{
    public float pos;
    public ParticleSystem PS;
    public void Start()
    {
        //get scale
        transform.localScale = new Vector3(transform.localScale.x, Random.Range(0.2f,0.6f), transform.localScale.z);
        pos = transform.localScale.y;
        //gm declare
        mowscript.God.MC.grasses.Add(this);
        //start grow
        StartCoroutine(Growback());
    }

    public void mow()
    {
        //stop previous grow, get rid of loop
        StopCoroutine(Growback());
        //cut
        pos = 0.1f;
        //emit particles
        PS.Emit(3);
        //start grow again
        StartCoroutine(Growback());
    }

    public void Update()
    {
        //set scale
        transform.localScale = new Vector3(transform.localScale.x, pos, transform.localScale.z);
    }
    
    public IEnumerator Growback()
    {
        //if shorter than 2
        while (pos <= 2)
        {
            //grow at random amount and random time
            pos += Random.Range(0.0005f, 0.001f);
            yield return new WaitForSeconds(Random.Range(1,4));
        }
    }
    
}
