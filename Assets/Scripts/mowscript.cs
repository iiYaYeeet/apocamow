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

    //gamemanager declare 
    public static class God
    {
        public static mowscript MC;
    }

    //gamemanager set
    void Awake()
    {
        God.MC = this;
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
            //offset get
            Vector3 offset = t.transform.position - transform.position;
            //if its further up then down , cont
            if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y)) continue;
            //get dist
            float dist = Vector3.Distance(transform.position, t.transform.position);
            //if its 1.5 away, cont
            if (dist < 1.5) continue;
            //if closer then current closest, set as closest
            if (dist < bestN)
            {
                bestN = dist;
            }
            //cut closest grass
            t.mow();
        }

        if (tootallgrass.Count >= 50)
        {
            if (dropped == false)
            {
                foreach (planecont P in planes)
                {
                    P.flyby();
                    dropped = true;
                }
            }
        }
    }

    public void bombdeto()
    {
        foreach (nukecont N in nukes)
        {
            N.detonate();
        }
    }
}
