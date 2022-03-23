using UnityEngine;
using UnityEngine.Audio;
/// <summary>
/// Writen By Jonáš Èerný, SID 1823654
/// </summary>


[System.Serializable]
public class Sound
{

    public string _name;
    public AudioMixerGroup _group;
    public AudioClip _clip;

    [Range(0f,1f)]
    public float _volume = 1f;

    [Range(0.1f,3f)]
    public float _pitch = 1f;

    [Range(0f,1f)]
    public float _spatialBlend = 0f;

    public float _maxDistance = 500f;

    public AudioRolloffMode _rolloffMode;

    [HideInInspector]
    public AudioSource _source;

    public bool _loop;
    public bool _ignoreAudioListenerPause;
}
