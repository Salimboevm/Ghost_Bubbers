using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] _menus;
    private AudioManager _AM;

    #region Making sure that the main menu is active
    private void Awake()
    {
        ButtonMainMenu();
    }

    private void Start()
    {
        _AM = AudioManager._instance;
        _AM.PlayMusic("MainMenu");
        _AM.StopMusic("Gameplay");
    }
    #endregion

    /// <summary>
    /// Plays the game
    /// </summary>
    public void ButtonPlay()
    {
        _AM.StopMusic("MainMenu");
        _AM.PlayMusic("Gameplay");

        SceneManager.LoadScene("GameplayScene");
    }

    /// <summary>
    /// Opens the difficulty menu
    /// </summary>
    public void ButtonDifficulties()
    {
        _menus [0].SetActive(false);
        _menus[1].SetActive(false);
        _menus[2].SetActive(true);
    }

    /// <summary>
    /// Opens the Settings Menu
    /// </summary>
    public void ButtonSettings()
    {
        _menus[0].SetActive(false);
        _menus[1].SetActive(true);
        _menus[2].SetActive(false);
    }

    /// <summary>
    /// Opens the main Menu
    /// </summary>
    public void ButtonMainMenu()
    {
        _menus[0].SetActive(true);
        _menus[1].SetActive(false);
        _menus[2].SetActive(false);
    }

    #region SetDifficulty
    #region Doesn't Work - no Time to fix it
    public void ButtonDifficulty(int ammountOfGhosts, int ammountOfPossessableObjects)
    {
        DifficultySetter dS = DifficultySetter._instance;
        dS.SetNumOfGhosts(ammountOfGhosts);
        dS.SetNumOfObjects(ammountOfPossessableObjects);
    }
    #endregion
    public void ButtonDifficultyEasy()
    {
        DifficultySetter dS = DifficultySetter._instance;
        dS.SetNumOfGhosts(3);
        dS.SetNumOfObjects(5);
    }

    public void ButtonDifficultyMedium()
    {
        DifficultySetter dS = DifficultySetter._instance;
        dS.SetNumOfGhosts(4);
        dS.SetNumOfObjects(7);
    }

    public void ButtonDifficultyHard()
    {
        DifficultySetter dS = DifficultySetter._instance;
        dS.SetNumOfGhosts(5);
        dS.SetNumOfObjects(8);
    } 
    #endregion

    /// <summary>
    /// Quits the game
    /// </summary>
    public void ButtonQuit()
    {
        Application.Quit();
    }

    public void ButtonSound()
    {
        _AM.PlaySFX("ButtonSound");
    }
}
