using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TV : Possessable
{
    [SerializeField] private GameObject staticScreen;

    public override void Possess()
    {
        staticScreen.SetActive(true);
    }

    public override void Unossess()
    {
        staticScreen.SetActive(false);
    }
}
