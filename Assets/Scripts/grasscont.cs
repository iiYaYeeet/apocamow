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
    public bool cuttable;
    public bool growing;
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
        if (cuttable == true)
        {
            growing = false;
            //cut
            pos = 0.1f;
            //emit particles
            PS.Emit(3);
            //start grow again
            if (growing == false)
            {
                StartCoroutine(Growback());
            }
            cuttable = false;
            mowscript.God.MC.tootallgrass.Remove(this);
        }
    }

    public void Update()
    {
        //set scale
        transform.localScale = new Vector3(transform.localScale.x, pos, transform.localScale.z);
    }
    
    public IEnumerator Growback()
    {
        yield return new WaitForSeconds(Random.Range(2, 4));
        growing = true;
        //if shorter than 2
        while (pos <= 2)
        {
            if (!growing) yield break;
            //grow at random amount and random time
            pos += Random.Range(0.003f, 0.013f);
            cuttable = true;
            yield return new WaitForSeconds(Random.Range(1,2f));
        }
        mowscript.God.MC.tootallgrass.Add(this);
    }
}
