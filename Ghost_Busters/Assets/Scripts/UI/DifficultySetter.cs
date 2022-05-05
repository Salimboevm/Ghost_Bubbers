using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySetter : MonoBehaviour
{
    public static DifficultySetter _instance;
    
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else 
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    #region GhostSetUp
    private int _numOfGhosts = 0;
    private int _numOfObjects = 0;
    public int GetNumOfGhosts() { return _numOfGhosts; }
    public void SetNumOfGhosts(int ammount) { _numOfGhosts = ammount; }

    public int GetNumOfObjects() { return _numOfObjects; }
    public void SetNumOfObjects(int objects) { _numOfObjects = objects; }
    #endregion
}
