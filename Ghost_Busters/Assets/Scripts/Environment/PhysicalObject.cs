using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalObject : Possessable
{
    public override void Possess()
    {
        Debug.Log("Possessed");
    }
}
