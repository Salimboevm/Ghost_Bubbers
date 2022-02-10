using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputFromPlayer : MonoBehaviour
{
    private static InputFromPlayer instance;

    public static InputFromPlayer Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
        inputMasterFromPlayer = new CharacterInputMaster();
    }
    private CharacterInputMaster inputMasterFromPlayer;
    private Vector2 moveInput;
    private Vector2 lookAroundInput;
    private Vector2 scrollWheelWhileToSwitchWeapons;
    KeyCode interactButtonPressed;
    public CharacterInputMaster InputMasterFromPlayer
    {
        get
        {
            return inputMasterFromPlayer;
        }
    }
  
    public Vector2 GetAndReturnPlayerMoveInput()
    {
        moveInput = inputMasterFromPlayer.Player.Movement.ReadValue<Vector2>();
        return moveInput;
    }
    public Vector2 GetAndReturnPlayerLookAroundInput()
    {
        lookAroundInput = inputMasterFromPlayer.Player.Look.ReadValue<Vector2>();
        return lookAroundInput;
    }
    public Vector2 GetAndReturnScrollWheelValue()
    {
        scrollWheelWhileToSwitchWeapons = inputMasterFromPlayer.Player.WeaponChange.ReadValue<Vector2>();
        return scrollWheelWhileToSwitchWeapons;
    }
    public void GetInteractionButtonPressed(System.Action action)
    {
        inputMasterFromPlayer.Player.Interact.performed += _ => action.Invoke();
    }
    private void OnEnable()
    {
        inputMasterFromPlayer.Enable();
    }
    private void OnDisable()
    {
        inputMasterFromPlayer.Disable();
    }
}
