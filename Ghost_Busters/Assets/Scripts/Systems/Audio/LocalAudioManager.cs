/// <summary>
/// Writen By Jonáš Èerný, SID 1823654
/// </summary>
using System.Collections.Generic;

public class LocalAudioManager : AudioManager 
{
    private void Awake()
    {
        SetAudioSettings();
    }

    /// <summary>
    /// Adds a list of sounds which this local AM should do
    /// </summary>
    /// <param name="addedSounds"></param>
    public void AddSounds(List<Sound> addedSounds)
    {
        _sounds = new Sound[addedSounds.Count];
        for (int i = 0; i < addedSounds.Count; i++)
        {
            _sounds[i] = addedSounds[i];
        }

        SetAudioSettings();
    }
}
