using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mowscript : MonoBehaviour
{
    //list
    public List<grasscont> grasses;
    public List<grasscont> tootallgrass;
    public List<nukecont> nukes; 
    public List<planecont> planes;
    public bool dropped;
    public AudioSource Bomb1, Bomb2, Plane1, Plane2, Mower;
    public AudioClip offgrass;
    public AudioClip ongrass;
    public AudioClip bombsincoming;
    public bool enginestarted;
    public bool onfield;
    public bool gamestarted;

    //gamemanager declare 
    public static class God
    {
        public static mowscript MC;
        public static playercontroller PC;
        public static uicontroller UI;
    }

    //gamemanager set
    void Awake()
    {
        God.MC = this;
    }

    public void gamestart()
    {
        Mower.Play();
        gamestarted = true;
    }
    void Update()
    {
        //find closest grass
        float bestN = 9999;
        Vector3 best = transform.position;
        foreach (grasscont t in grasses)
        {
            // x and z limit
            if (t.transform.position.z >= transform.position.z + 2f ||
                t.transform.position.z <= transform.position.z - 2f) continue;
            if (t.transform.position.x >= transform.position.x + 2f ||
                t.transform.position.x <= transform.position.x - 2f) continue;
            //get dist
            float dist = Vector3.Distance(transform.position, t.transform.position);
            //if its 1.5 away, cont
            if (dist < 1.2) continue;
            //if closer then current closest, set as closest
            if (dist < bestN)
            {
                bestN = dist;
            }
            //cut closest grass
            t.mow();
        }
        if (Mower.isPlaying == false && gamestarted==true)
        {
            enginestarted = true;
            Mower.Play();
        }
        if (enginestarted == true)
        {
            if (onfield == false)
            {
                Mower.clip = offgrass;
            }
            if (onfield == true)
            {
                Mower.clip = ongrass;
            }
        }

        if (tootallgrass.Count >= 50)
        {
            if (dropped == false)
            {
                foreach (planecont P in planes)
                {
                    P.flyby();
                    Plane1.Play();
                    Plane2.Play();
                    dropped = true;
                }
                Mower.PlayOneShot(bombsincoming);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        onfield = true;
    }
    public void OnTriggerExit(Collider other)
    {
        onfield = false;
    }
    

    public void bombdeto()
    {
        Mower.Stop();
        foreach (nukecont N in nukes)
        {
            N.detonate();
            Bomb1.Play();
            Bomb2.Play();
        }
    }
}
