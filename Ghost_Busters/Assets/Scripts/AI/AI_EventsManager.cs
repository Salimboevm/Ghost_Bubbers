using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_EventsManager : MonoBehaviour
{
    #region Singleton
    public static AI_EventsManager _instance;

    void Awake()
    {
        _instance = this;
    }
    #endregion

    #region Events

    #region OnPossessed
    public static event System.Action<int, int> OnPossessed;
    public void ObjectPossessed(int objectID, int ghostID)
    {
        if (OnPossessed != null)
            OnPossessed(objectID, ghostID);
    }
    #endregion

    #region OnCleared
    public static event System.Action<int> OnCleared;
    public void ObjectCleared(int objectID)
    {
        if (OnCleared != null)
            OnCleared(objectID);
    }
    #endregion 

    #endregion
}
