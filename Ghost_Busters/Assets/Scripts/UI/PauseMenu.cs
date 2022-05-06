using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu _instance;

    [SerializeField] private GameObject[] _menus;
    [SerializeField] private PlayerInput _plIn;
    private InputAction _pause;

    #region Keyboard Input
    private void Awake()
    {
        _instance = this;

        _pause = _plIn.actions["Pause"];
    }

    private void OnEnable()
    {
        _pause?.Enable();
    }
    private void OnDisable()
    {
        _pause?.Disable();
    }
    #endregion

    private void Update()
    {
        if (_pause.triggered)
            TogglePause();
    }

    public void TogglePause()
    {
        if (!GameManager.instance.GetIsGamePaused())
        {
            GameManager.instance.SetIsGamePaused(true);

            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;

            _menus[0].SetActive(true);
            _menus[1].SetActive(false);
        }
        else
        {
            GameManager.instance.SetIsGamePaused(false);

            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;

            _menus[0].SetActive(false);
            _menus[1].SetActive(false);
        }
    }

    public void ButtonMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    public void WinGame()
    {
        Debug.Log("We Won!");
        GameManager.instance.SetIsGamePaused(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;

        _menus[0].SetActive(false);
        _menus[1].SetActive(true);
    }

    public void ButtonSound()
    {
        AudioManager._instance.PlaySFX("Button");
    }
}
