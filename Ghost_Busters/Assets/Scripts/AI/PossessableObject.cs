using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PossessableObject
{
    public GameObject _object;
    public int _objectID = -1;
    public bool _possessed = false;
    public bool _puzzleSolved = false;
    public int _ghostID = -1;
}
