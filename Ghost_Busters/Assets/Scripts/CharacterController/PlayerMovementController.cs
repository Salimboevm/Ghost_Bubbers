using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    InputFromPlayer playerInputInstance;

    [SerializeField]
    private float moveSpeed = 6f;
    private Vector2 movement;
    private Transform cameraTransform;
    private CharacterController characterController;

    #region NotGroundedVariables
    //if player is not on the ground
    [Header("Player Is On Air")]
    private Vector3 _playerVelocity;
    private bool _isPlayerGrounded;
    private float _gravityValue = -9.81f;
    #endregion
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Start()
    {
        playerInputInstance = InputFromPlayer.Instance;
        cameraTransform = Camera.main.transform;
    }
    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        CheckPlayerIsGrounded();
        movement = playerInputInstance.GetPlayerMoveInput();
        Vector3 move = new Vector3(movement.x,0f,movement.y);
        move = (cameraTransform.forward * move.z) + (cameraTransform.right * move.x);
        move.y = 0;
        characterController.Move(move * moveSpeed);
    }
    void CheckPlayerIsGrounded()
    {
        _isPlayerGrounded = characterController.isGrounded;
        if (_isPlayerGrounded && _playerVelocity.y<0)
        {
            _playerVelocity.y = 0;
        }
        SetPlayerToGround();
    }
    void SetPlayerToGround()
    {
        _playerVelocity.y += _gravityValue * Time.deltaTime;
        characterController.Move(_playerVelocity * Time.deltaTime);
    }
}
