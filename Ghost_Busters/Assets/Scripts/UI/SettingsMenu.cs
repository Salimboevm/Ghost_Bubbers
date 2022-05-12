using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

#region Author
/// <summary>
/// Writen By Jonáš Èerný, SID 1823654
/// </summary> 
#endregion

public class SettingsMenu : MonoBehaviour
{
    #region Audio Variables
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] TextMeshProUGUI[] _audioPercent;
    [SerializeField] Slider[] _audioSliders;
    [SerializeField] float _minVolume = 50f;
    #endregion

    #region Resolution Variables
    Resolution[] _resolutions;
    [SerializeField] TMP_Dropdown _resolutionDropdown;
    [SerializeField] Toggle _fullscreen;
    #endregion

    #region Saving Variables
    private bool _isFullscreen;
    private float _masterVolume;
    private float _musicVolume;
    private float _sfxVolume;
    private Resolution _chosenResolution; 
    #endregion


    private void Start()
    {
        #region Resolution + Saved Settings Loading
        #region Data needed for the Resolution Dropdown
        //We get Avaibale resolutions
        _resolutions = Screen.resolutions;
        //and clear what we had in the drop down menu
        _resolutionDropdown.ClearOptions();
        #endregion

        #region Variables needed for the Dropdown menu
        //Create a String list for the drop down menu
        List<string> options = new List<string>();

        //Create an int variable to save and apply our correct resolution on start
        int currentResolutionIndex = 0;
        #endregion

        #region Creating text for Dropdown
        //We add every resolution width and height into the string list and also once, we find the correct resolution we assign it to turn on
        for (int i = 0; i < _resolutions.Length; i++)
        {
            if (i < _resolutions.Length - 1 && _resolutions[i].width != _resolutions[i + 1].width && _resolutions[i].height != _resolutions[i + 1].height) // Making Sure we don't assign 2 resolutions twice
            {
                string option = _resolutions[i].width + " x " + _resolutions[i].height;
                options.Add(option);

                #region If we have previously saved settings
                if (PlayerPrefs.GetString("settings") != "")
                {
                    #region Load in the Saved Settings
                    //We load in the settings
                    string jsonString = PlayerPrefs.GetString("settings");
                    SaveSettingsObject saveSettings = JsonUtility.FromJson<SaveSettingsObject>(jsonString);
                    #endregion

                    #region Set the correct resolution
                    if (_resolutions[i].width == saveSettings.resolutionWidth && _resolutions[i].height == saveSettings.resolutionHeight)
                    {
                        currentResolutionIndex = i;
                    }
                    #endregion

                    #region Set Audio-Settings
                    //Set the Audio Settings and their Visuals
                    _audioSliders[0].value = saveSettings.masterVolume;
                    SetMasterVolume();

                    _audioSliders[2].value = saveSettings.musicVolume;
                    SetMusicVolume();

                    _audioSliders[1].value = saveSettings.sfxVolume;
                    SetSFXVolume();
                    #endregion

                    #region Set FullScreen
                    //set Fullscreen
                    _fullscreen.isOn = saveSettings.fullscreen;
                    SetFullscreen();
                    #endregion
                }
                #endregion
                #region If we have unsaved settings
                else if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
                #endregion 
            }
        }
        #endregion

        #region Assigning Dropdown buttons and correct resolution
        //we add all the options and set our current resolution
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
        #endregion
        #endregion

        #region Setting Minimum Value - DEVELOPLMENT STAGE ONLY
        for (int i = 0; i < 3; i++)
        {
            _audioSliders[i].minValue = -_minVolume;
        } 
        #endregion

    }

    #region Audio Functions
    /// <summary>
    /// Sets the master volume
    /// </summary>
    public void SetMasterVolume()
    {
        _audioMixer.SetFloat("MasterVolume", _audioSliders[0].value);
        _audioPercent[0].text = ((100 - (_audioSliders[0].value / _minVolume) * -100f)).ToString() + "%";


        //Checks if there is supposed to be no sound
        if (_audioSliders[0].value == -_minVolume)
        {
            _audioMixer.SetFloat("MasterVolume", -80f);
            _masterVolume = -80f;
        }
        else
            _masterVolume = _audioSliders[0].value;
    }

    /// <summary>
    /// Sets the Music Volume
    /// </summary>
    public void SetMusicVolume()
    {
        _audioMixer.SetFloat("MusicVolume", _audioSliders[2].value);
        _audioPercent[2].text = ((100 - (_audioSliders[2].value / _minVolume) * -100f)).ToString() + "%";

        //Checks if there is supposed to be no sound
        if (_audioSliders[2].value == -_minVolume)
        {
            _audioMixer.SetFloat("MusicVolume", -80f);
            _musicVolume = -80f;
        }
        else
            _musicVolume = _audioSliders[2].value;
    }

    /// <summary>
    /// Sets the SFX Volume
    /// </summary>
    public void SetSFXVolume()
    {
        _audioMixer.SetFloat("SFXVolume", _audioSliders[1].value);
        _audioPercent[1].text = ((100 - (_audioSliders[1].value / _minVolume) * -100f)).ToString() + "%";

        //Checks if there is supposed to be no sound
        if (_audioSliders[1].value == -_minVolume)
        {
            _audioMixer.SetFloat("SFXVolume", -80f);
            _sfxVolume = -80f;
        }
        else
            _sfxVolume = _audioSliders[1].value;
    }
    #endregion

    #region Video Functions
    /// <summary>
    /// Toggles FullScreen mode
    /// </summary>
    /// <param name="isFullscreen"></param>
    public void SetFullscreen()
    {
        Screen.fullScreen = _fullscreen;

        _isFullscreen = _fullscreen;
    }

    /// <summary>
    /// Sets resolution to a chosen ratio
    /// </summary>
    public void SetResolution()
    {
        Resolution resolution = _resolutions[_resolutionDropdown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        _chosenResolution = _resolutions[_resolutionDropdown.value];
    }
    #endregion

    #region Saving
    /// <summary>
    /// Saves the current Settings
    /// </summary>
    public void SaveSettingsButton()
    {
        SaveSettingsObject saveObject = new SaveSettingsObject { masterVolume = _masterVolume, fullscreen = _isFullscreen, musicVolume = _musicVolume, sfxVolume = _sfxVolume, resolutionWidth = _chosenResolution.width, resolutionHeight = _chosenResolution.height };
        string json = JsonUtility.ToJson(saveObject);
        PlayerPrefs.SetString("settings", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("settings"));
    }

    /// <summary>
    /// A class with all the Game Settings
    /// </summary>
    private class SaveSettingsObject
    {
        public float masterVolume;
        public float musicVolume;
        public float sfxVolume;
        public bool fullscreen;
        public int resolutionWidth;
        public int resolutionHeight;
    } 
    #endregion
}
