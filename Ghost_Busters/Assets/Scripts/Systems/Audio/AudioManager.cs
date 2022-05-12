using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Writen By Jon� �ern�, SID 1823654
/// </summary>

public class AudioManager : MonoBehaviour
{
    public Sound[] _sounds;

    public static AudioManager _instance;

    // Start is called before the first frame update
    void Awake()
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

        #region Sets each sound's properties
        SetAudioSettings();
        #endregion
    }

    /// <summary>
    /// Updates the settings for each audio in the list
    /// </summary>
    public void SetAudioSettings()
    {
        foreach (Sound s in _sounds)
        {
            //Audio Components
            s._source = gameObject.AddComponent<AudioSource>();
            s._source.clip = s._clip;

            //AudioManagement
            s._source.outputAudioMixerGroup = s._group;

            //3D Sound
            s._source.spatialBlend = s._spatialBlend;
            s._source.maxDistance = s._maxDistance;
            s._source.rolloffMode = s._rolloffMode;

            //AudioValues
            s._source.volume = s._volume;
            s._source.pitch = s._pitch;
            s._source.playOnAwake = s._playOnAwake;
            s._source.loop = s._loop;
            s._source.ignoreListenerPause = s._ignoreAudioListenerPause;
        }
    }

    #region Play/Stop Audio Functions
    /// <summary>
    /// Plays the sound with given name
    /// </summary>
    /// <param name="name"></param>
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound._name == name);

        if (s == null) { Debug.LogError("No sound with the name '" + name + "' was found"); return; }

        s._source.Play();


        //Debug.Log("Song '" + name + "' is played now");
    }

    /// <summary>
    /// Stops the sound with given name
    /// </summary>
    /// <param name="name"></param>
    public void StopMusic(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound._name == name);

        if (s == null) { Debug.LogError("No sound with the name '" + name + "' was found"); return; }

        s._source.Stop();

        //Debug.Log("Song '" + name + "' is stopped now");
    }

    /// <summary>
    /// Plays "Oneshot" of the sound with the given name
    /// </summary>
    /// <param name="name"></param>
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound._name == name);

        if (s == null) { Debug.LogError("No sound with the name '" + name + "' was found"); return; }

        s._source.PlayOneShot(s._source.clip);

        //Debug.Log("Sound '" + name + "' was played now");
    } 

    /// <summary>
    /// Plays a Random Sound from a given list of strings (With each sounds having the same chance to play, unless it has been added multiple times)
    /// </summary>
    /// <param name="sounds"></param>
    public void PlayRandomSFX(List<string> sounds)
    {
        int ranNum = Mathf.CeilToInt(UnityEngine.Random.Range(0, sounds.Count-1));

        for (int i = 0; i < sounds.Count; i++)
        {
            if(i == ranNum)
            {
                PlaySFX(sounds[i]);
                break;
            }
        }
    }
    #endregion

    #region Get 1 Sound
    public Sound GetSound(int id) { return _sounds[id]; } 
    #endregion
}
