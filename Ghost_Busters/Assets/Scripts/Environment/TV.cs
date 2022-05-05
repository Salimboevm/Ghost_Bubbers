using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : Possessable
{
    [SerializeField] private Animator anim;

    public override void Possess()
    {
        anim.SetBool("Active", true);
    }

    public override void Unpossess()
    {
        anim.SetBool("Active", false);
    }
}
