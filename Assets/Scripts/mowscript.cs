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
        foreach (grasscont t in grasses)
        {
            if (t.transform.position.z >= transform.position.z + 2f ||
                t.transform.position.z <= transform.position.z - 2f) continue;
            if (t.transform.position.x >= transform.position.x + 2f ||
                t.transform.position.x <= transform.position.x - 2f) continue;
            Vector3 offset = t.transform.position - transform.position;
            if (Mathf.Abs(offset.x) > Mathf.Abs(offset.y)) continue;
            float dist = Vector3.Distance(transform.position, t.transform.position);
            if (dist < 1.5) continue;
            if (dist < bestN)
            {
                bestN = dist;
            }
            t.mow();
        }
    }
}
