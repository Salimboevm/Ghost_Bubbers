using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;
using UnityEngine;

public class InputFromPlayer : MonoBehaviour
{
    public static InputFromPlayer Instance
    {
        get
        {
            return _instance;
        }
    }

    public Action _startShooting;
    public Action _stopShooting;

    private static InputFromPlayer _instance;
    private CharacterInputMaster _inputMasterFromPlayer;
    private Vector2 _moveInput;
    private Vector2 _lookAroundInput;
    private Vector2 _screenPoint;
    private Vector2 _scrollWheelWhileToSwitchWeapons;
    KeyCode _interactButtonPressed;
    public CharacterInputMaster InputMasterFromPlayer
    {
        get
        {
            return _inputMasterFromPlayer;
        }
    }
    private void Awake()
    {
        _instance = this;
        _inputMasterFromPlayer = new CharacterInputMaster();
    }
  
    public Vector2 GetPlayerMoveInput()
    {
        _moveInput = _inputMasterFromPlayer.Player.Movement.ReadValue<Vector2>();
        return _moveInput;
    }
    public Vector2 GetPlayerLookAroundInput()
    {
        _lookAroundInput = _inputMasterFromPlayer.Player.Look.ReadValue<Vector2>();
        return _lookAroundInput;
    }
    public Vector2 GetScrollWheelValue()
    {
        _scrollWheelWhileToSwitchWeapons = _inputMasterFromPlayer.Player.WeaponChange.ReadValue<Vector2>();
        return _scrollWheelWhileToSwitchWeapons;
    }
    public void GetChannelIncreasedChangeValue(Action action)
    {
        _inputMasterFromPlayer.Player.ChannelIncrease.performed += _=>action?.Invoke();
    }
    public void GetChannelDecreasedChangeValue(Action action)
    {
        _inputMasterFromPlayer.Player.DecreaseChannel.performed += _=>action?.Invoke();
    }
    public void GetInteractionButtonPressed(Action action)
    {
        _inputMasterFromPlayer.Player.Interact.performed += _ => action?.Invoke();
    }
    public void GetShootButtonStarted(Action action)
    {
        _inputMasterFromPlayer.Player.Shoot.performed += act => action?.Invoke();
    }
    private void OnEnable()
    {
        _inputMasterFromPlayer.Enable();
    }
    private void OnDisable()
    {
        _inputMasterFromPlayer.Disable();
    }
}
