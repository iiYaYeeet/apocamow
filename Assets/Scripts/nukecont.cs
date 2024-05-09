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
    public Rigidbody house,fan;
    public bool deto = false ;

    public void Start()
    {
        mowscript.God.MC.nukes.Add(this);
        transform.position = new Vector3(Random.Range(-200, 200), 2, Random.Range(-200, 200));
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
            house.AddForce(100,50,-5,ForceMode.Impulse);
            fan.AddForce(100,50,-5,ForceMode.Impulse);
    }
    public IEnumerator pop()
    {
        yield return new WaitForSeconds(2);
        while (flash.range <= 20000)
        {
            flash.range *= 1.3f;
            cloud.transform.localScale = new Vector3(cloud.transform.localScale.x * 1.1f,
                cloud.transform.localScale.y * 1.1f, cloud.transform.localScale.z * 1.1f);
            yield return null;
        }
        var emissionModule = PS.emission;
        emissionModule.enabled = false;
        cam.transform.localPosition = Vector3.zero;
        yield return new WaitForSeconds(3);
        mowscript.God.UI.gameover();
        shakeamount = 0;
    }

    public void Update()
    {
        if (deto == true)
        {
            cam.transform.localPosition = new Vector3(Random.Range(shakeamount, -shakeamount),
                Random.Range(shakeamount, -shakeamount) + 0.51f, Random.Range(shakeamount, -shakeamount));
        }
    }
}
