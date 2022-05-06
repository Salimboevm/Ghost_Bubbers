using System.Collections.Generic;
using UnityEngine;
using TMPro;

#region Author
/// <summary>
/// Writen By Jonáš Èerný, SID 1823654
/// </summary> 
#endregion

public class HighScoreMenu : MonoBehaviour
{
    /// <summary>
    /// Variable for grabbing new scores
    /// </summary>
    ScoreGrabber SG;

    #region Variables for High-score list creation
    [SerializeField] private Transform _entryContainer;
    [SerializeField] private Transform _entryTemplate;
    private List<HighscoreEntry> _highscoreEntryList;
    private List<Transform> _highscoreEntryTransformList; 
    #endregion

    private void Awake()
    {
        //turning off the template
        _entryTemplate.gameObject.SetActive(false);

        #region Creating the High-Score Table
        #region If we don't have any HS table saved
        if (PlayerPrefs.GetString("highscoreTable") == "")
        {
            //Create a new High-Score List with 10 spots
            _highscoreEntryList = new List<HighscoreEntry>()
            {
                new HighscoreEntry{score = 0, name = "Player"},
                new HighscoreEntry{score = 0, name = "Player"},
                new HighscoreEntry{score = 0, name = "Player"},
                new HighscoreEntry{score = 0, name = "Player"},
                new HighscoreEntry{score = 0, name = "Player"},
                new HighscoreEntry{score = 0, name = "Player"},
                new HighscoreEntry{score = 0, name = "Player"},
                new HighscoreEntry{score = 0, name = "Player"},
                new HighscoreEntry{score = 0, name = "Player"},
                new HighscoreEntry{score = 0, name = "Player"},
            };

            #region Save the newly created table
            //Save the Table
            Highscores highscores = new Highscores { highscoreEntryList = _highscoreEntryList };
            string json = JsonUtility.ToJson(highscores);
            PlayerPrefs.SetString("highscoreTable", json);
            PlayerPrefs.Save();
            #endregion
        } 
        #endregion
        else
        {
            LoadHighScores();
        }
        #endregion

        
    }

    #region Automated Score-Adding
    private void Start()
    {
        //Getting the ScoreGrabber
        SG = ScoreGrabber._instance;


        //Grabbing the new ScoreEntry
        AddHighscoreEntry(SG.AssignScore(), SG.AssignName());

        #region Creating the Transform List/The UI High Score Table
        _highscoreEntryTransformList = new List<Transform>();

        foreach (HighscoreEntry highscoreEntry in _highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, _entryContainer, _highscoreEntryTransformList);
        }
        #endregion
    }
    #endregion

    #region High-Scores Functionality
    #region High-Score Classes
    /// <summary>
    /// adds an entry to the list of highscores
    /// </summary>
    /// <param name="highscoreEntry"></param>
    /// <param name="container"></param>
    /// <param name="transformList"></param>
    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 22f;

        Transform entryTransform = Instantiate(_entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);


        int rank = transformList.Count + 1;
        string rankString = rank + ".";
        entryTransform.Find("Pos").GetComponent<TextMeshProUGUI>().text = rankString;

        string name = highscoreEntry.name;
        entryTransform.Find("Name").GetComponent<TextMeshProUGUI>().text = name;

        int score = highscoreEntry.score;
        entryTransform.Find("Scor").GetComponent<TextMeshProUGUI>().text = score.ToString();

        transformList.Add(entryTransform);
    }

    /// <summary>
    /// A list of HighScore Entries
    /// </summary>
    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    /// <summary>
    /// A single high-score entry
    /// </summary>
    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
    #endregion

    /// <summary>
    /// Adds a new score entry in the correct position
    /// </summary>
    /// <param name="score"></param>
    /// <param name="name"></param>
    private void AddHighscoreEntry(int score, string name)
    {
        //assigning the name and score as an entry
        HighscoreEntry newEntry = new HighscoreEntry { score = score, name = name };

        #region Geting the table of High-scores
        //Geting the table of High-scores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        #endregion

        #region Sorting the High-Scores just in case
        //Sort the list by score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    //swap
                    HighscoreEntry temp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = temp;
                }
            }
        }
        #endregion


        #region Adding and placing the new score
        //if we place on the board
        if (highscores.highscoreEntryList[9].score < score)
        {
            for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
            {
                //if the score we are looking at is smaller...
                if (highscores.highscoreEntryList[i].score < score)
                {
                    //...Save the old score and...
                    HighscoreEntry temp = highscores.highscoreEntryList[i];
                    //...Put the new entry on that position...
                    highscores.highscoreEntryList[i] = newEntry;

                    //...and we move every other score down
                    for (int a = i + 1; a < highscores.highscoreEntryList.Count; a++)
                    {
                        HighscoreEntry temp2 = highscores.highscoreEntryList[a];
                        highscores.highscoreEntryList[a] = temp;
                        temp = temp2;
                    }
                    break; //we don't need to continue
                }
            }
        }
        #endregion

        #region Save the new High-Scores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        LoadHighScores();
        #endregion
    }
    #endregion

    /// <summary>
    /// Loads the saved Data
    /// </summary>
    private void LoadHighScores()
    {
        _highscoreEntryList = new List<HighscoreEntry>();
        _highscoreEntryList.Clear();

        #region Loading High-Scores
        //Load a saved High-Score List
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        _highscoreEntryList = highscores.highscoreEntryList;
        #endregion
    }
}
