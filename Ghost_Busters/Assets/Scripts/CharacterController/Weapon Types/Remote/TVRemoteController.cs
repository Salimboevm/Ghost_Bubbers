using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVRemoteController : WeaponTypes
{
    private const uint MAX_NUMBER_OF_CHANNELS = 4;
    
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
        Mathf.Clamp(_currentChannel, 0, 3);
        print(_currentChannel);
    }
    void DecreaseCurrentChannelValue()
    {
        _currentChannel--;
        Mathf.Clamp(_currentChannel, 0, 3);
        print(_currentChannel);
    }
    protected override void RemoteController()
    {
        //when channel number is changed
        //invoke action to change tv screen video
        //play animation of player 
    }
}
