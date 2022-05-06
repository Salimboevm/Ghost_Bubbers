using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] _menus;
    private AudioManager _audioManager;

    #region Making sure that the main menu is active
    private void Awake()
    {
        ButtonMainMenu();
    }
    #endregion

    #region Audio SetUp
    private void Start()
    {
        _audioManager = AudioManager._instance;

        _audioManager.StopMusic("Gameplay");
        _audioManager.PlayMusic("MainMenu");
    } 
    #endregion

    /// <summary>
    /// Plays the game
    /// </summary>
    public void ButtonPlay()
    {
        _audioManager.StopMusic("MainMenu");
        _audioManager.PlayMusic("Gameplay");

        SceneManager.LoadScene("GameplayScene");
    }

    #region Menu Buttons
    /// <summary>
    /// Opens the difficulty menu
    /// </summary>
    public void ButtonDifficulties()
    {
        _menus[0].SetActive(false);
        _menus[1].SetActive(false);
        _menus[2].SetActive(true);
        _menus[3].SetActive(false);
    }

    /// <summary>
    /// Opens the Settings Menu
    /// </summary>
    public void ButtonSettings()
    {
        _menus[0].SetActive(false);
        _menus[1].SetActive(true);
        _menus[2].SetActive(false);
        _menus[3].SetActive(false);
    }

    /// <summary>
    /// Opens the main Menu
    /// </summary>
    public void ButtonMainMenu()
    {
        _menus[0].SetActive(true);
        _menus[1].SetActive(false);
        _menus[2].SetActive(false);
        _menus[3].SetActive(false);
    }

    /// <summary>
    /// Opens the controls menu
    /// </summary>
    public void ButtonTutorialMenu()
    {
        _menus[0].SetActive(false);
        _menus[1].SetActive(false);
        _menus[2].SetActive(false);
        _menus[3].SetActive(true);
    } 
    #endregion

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
        dS.SetNumOfObjects(6);
    }

    public void ButtonDifficultyHard()
    {
        DifficultySetter dS = DifficultySetter._instance;
        dS.SetNumOfGhosts(5);
        dS.SetNumOfObjects(7);
    } 
    #endregion

    /// <summary>
    /// Quits the game
    /// </summary>
    public void ButtonQuit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Plays the button sound
    /// </summary>
    public void ButtonSound()
    {
        _audioManager.PlaySFX("Button");
    }
}
