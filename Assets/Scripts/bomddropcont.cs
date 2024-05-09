using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomddropcont : MonoBehaviour
{
    public AudioSource AS;
    public void deto()
    {
        AS.Stop();
        mowscript.God.MC.bombdeto();
    }

    public void drop()
    {
        AS.Play();
    }
}
