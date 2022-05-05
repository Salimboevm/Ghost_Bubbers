using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    /// <summary>
    /// ID of the object which this puzzle will solve
    /// </summary>
    private int objectID = 0;

    #region Get/Set
    public int GetObjectID() { return objectID; }
    public void SetObjectID(int objectOfID) { objectID = objectOfID; } 
    #endregion
}
