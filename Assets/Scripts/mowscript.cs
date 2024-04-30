using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mowscript : MonoBehaviour
{
    public List<grasscont> grasses;   

    public static class God
    {
        public static mowscript MC;
    }

    void Awake()
    {
        God.MC = this;
        Debug.Log(God.MC);
    }
    void Update()
    {
        float bestN = 9999;
        Vector3 best = transform.position;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            foreach (grasscont t in grasses)
            {
                if (t.transform.position.z >= transform.position.z+2f || t.transform.position.z <= transform.position.z-2f) continue;
                Debug.Log("line 0 clear");
                if (t.transform.position.x >= transform.position.x+2f || t.transform.position.x <= transform.position.x-2f) continue;
                Debug.Log("line 1 clear");
                Vector3 offset = t.transform.position - transform.position;
                Debug.Log("line 2 clear");
                if(Mathf.Abs(offset.x) > Mathf.Abs(offset.y)) continue;
                Debug.Log("line 3 clear");
                float dist = Vector3.Distance(transform.position, t.transform.position);
                Debug.Log("line 4 clear");
                if (dist < 1.5) continue;
                Debug.Log("line 5 clear");
                if (dist < bestN)
                {
                    bestN = dist;
                }
                Debug.Log("this shit actually worked what the fuck"+t);
                t.mow();
            }
        }
    }
}
