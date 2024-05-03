using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class nukecont : MonoBehaviour
{
    public Light flash;
    public Light halo;
    public GameObject cloud;
    public MeshRenderer MR;
    public ParticleSystem PS;

    public void Start()
    {
        mowscript.God.MC.nukes.Add(this);
    }
    

    public void detonate()
    {
            flash.enabled = true;
            halo.enabled = true;
            halo.range = 30;
            MR.enabled = true;
            flash.range += 1;
            var emissionModule = PS.emission;
            emissionModule.enabled = true;
            PS.Emit(500);
            StartCoroutine(pop());
    }
    public IEnumerator pop()
    {
        yield return new WaitForSeconds(1);
        while (flash.range <= 100000)
        {
            Debug.Log("called");
            flash.range *= 1.5f;
            Debug.Log("flashgrow");
            cloud.transform.localScale = new Vector3(cloud.transform.localScale.x * 1.1f,
                cloud.transform.localScale.y * 1.1f, cloud.transform.localScale.z * 1.1f);
            Debug.Log("cloudgrow");
            yield return null;
        }
        var emissionModule = PS.emission;
        emissionModule.enabled = false;
        yield return new WaitForSeconds(2);
        while (halo.intensity >= 1)
        {
            Mathf.Lerp(halo.intensity, 0, 1);
            cloud.transform.localScale = new Vector3(cloud.transform.localScale.x / 1.001f,
                cloud.transform.localScale.y / 1.001f, cloud.transform.localScale.z / 1.01f);
            yield return null;
        }
    }
}
