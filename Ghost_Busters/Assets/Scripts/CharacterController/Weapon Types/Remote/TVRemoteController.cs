using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class TVRemoteController : WeaponTypes
{
    System.Random _systemRandomNumberGenerator = new System.Random();
    private const int MAX_NUMBER_OF_CHANNELS = 4;
    [SerializeField]
    private VideoClip[] _newPlayingClip;
    [SerializeField]
    private VideoPlayer _tvVideoPlayer;
    private uint _onWhichChannelGhostHided = 0;
    private uint _currentChannel;

    private void Start()
    {
        InputFromPlayer.Instance.GetChannelIncreasedChangeValue(IncreaseCurrentChannelValue);
        InputFromPlayer.Instance.GetChannelDecreasedChangeValue(DecreaseCurrentChannelValue);
    }

    void IncreaseCurrentChannelValue()
    {
        _currentChannel++;
        RemoteController();
        Mathf.Clamp(_currentChannel, 0, 3);
    }
    void DecreaseCurrentChannelValue()
    {
        _currentChannel--;
        RemoteController();
        Mathf.Clamp(_currentChannel, 0, 3);
        
    }
    protected override void RemoteController()
    {
        ChangePlayingVideo(_newPlayingClip[_currentChannel]);
        
    }
    private void HideGhostInTV()
    {
        int randomNumber = _systemRandomNumberGenerator.Next(1,MAX_NUMBER_OF_CHANNELS);
        SetHidedGhostChannelNumber((uint)randomNumber);
    }
    public void SetHidedGhostChannelNumber(uint channelNumber)
    {
        _onWhichChannelGhostHided = channelNumber;
    }
    private void ChangePlayingVideo(VideoClip newPlayingClip)
    {
        _tvVideoPlayer.clip = newPlayingClip;
    }
}
