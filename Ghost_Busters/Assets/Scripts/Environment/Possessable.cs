using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Possessable : MonoBehaviour
{
    public int _id = -1;
    public abstract void Possess();

    public abstract void Unpossess();
}
