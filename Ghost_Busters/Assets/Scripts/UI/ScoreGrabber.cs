using UnityEngine;
/// <summary>
/// Writen By Jonáš Èerný, SID 1823654
/// </summary>

public class ScoreGrabber : MonoBehaviour
{
    public static ScoreGrabber _instance;

    private int _score;
    private string _name;

    private void Awake()
    {
        #region Singleton
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        #endregion

        #region Default Score and Name
        _score = 0;
        _name = "Player"; 
        #endregion
    }

    #region Score Functions
    /// <summary>
    /// Sets the score to the latest saved and then sets it to the default one
    /// </summary>
    /// <param name="score"></param>
    public void SetScore(int score)
    {
        _score = score;
    }

    /// <summary>
    /// Gets a new score
    /// </summary>
    /// <returns></returns>
    public int AssignScore()
    {
        int tempScore = _score;
        _score = 0;
        return tempScore;
    }

    /// <summary>
    /// Gets the current score stored in the Grabber
    /// </summary>
    /// <returns></returns>
    public int ReadScore() { return _score; }
    #endregion


    #region Name Functions
    /// <summary>
    /// Sets the name to the latest saved and then sets it to the default one
    /// </summary>
    /// <param name="name"></param>
    public void SetName(string name)
    {
        _name = name;
    }

    /// <summary>
    /// Gets a new Name
    /// </summary>
    /// <returns></returns>
    public string AssignName()
    {
        string tempName = _name;
        _name = "Player";
        return tempName;
    } 

    /// <summary>
    /// Gets the current name stored in the Grabber
    /// </summary>
    /// <returns></returns>
    public string ReadName() { return _name; }
    #endregion
}
