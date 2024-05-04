using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planecont : MonoBehaviour
{
    public Animator anim;
    public AnimationClip fly;
    public void Start()
    {
        mowscript.God.MC.planes.Add(this);
    }

    public void flyby()
    {
        anim.Play("dropbomb");
    }
}
