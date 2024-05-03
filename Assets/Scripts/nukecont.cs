using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class nukecont : MonoBehaviour
{
    public Light flash;
    public Light halo;
    public GameObject cloud;
    public MeshRenderer MR;
    public ParticleSystem PS;
    public GameObject cam;
    public float shakeamount;

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
            shakeamount = 0.2f;
            StartCoroutine(pop());
    }
    public IEnumerator pop()
    {
        yield return new WaitForSeconds(1);
        while (flash.range <= 100000)
        {
            flash.range *= 1.5f;
            cloud.transform.localScale = new Vector3(cloud.transform.localScale.x * 1.1f,
                cloud.transform.localScale.y * 1.1f, cloud.transform.localScale.z * 1.1f);
            yield return null;
        }
        var emissionModule = PS.emission;
        emissionModule.enabled = false;
        cam.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(2);
        while (halo.intensity >= 1)
        {
            Mathf.Lerp(halo.intensity, 0, 1);
            Mathf.Lerp(shakeamount, 0, 5);
            cloud.transform.localScale = new Vector3(cloud.transform.localScale.x / 1.001f,
                cloud.transform.localScale.y / 1.001f, cloud.transform.localScale.z / 1.01f);
            yield return null;
        }
        shakeamount = 0;
    }

    public void Update()
    {
        cam.transform.localPosition = new Vector3(Random.Range(shakeamount,-shakeamount),Random.Range(shakeamount,-shakeamount)+0.51f,Random.Range(shakeamount,-shakeamount));
    }
}
