using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    } 
    #endregion

    //Pausing Boolean
    [SerializeField] private bool _isGamePaused = false;

    #region Get/Set
    public bool GetIsGamePaused() { return _isGamePaused; }
    public void SetIsGamePaused(bool state) { _isGamePaused = state; } 
    #endregion
}
