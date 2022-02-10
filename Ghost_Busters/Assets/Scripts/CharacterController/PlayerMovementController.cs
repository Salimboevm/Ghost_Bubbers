using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class PlayerMovementController : MonoBehaviour
{
    InputFromPlayer playerInputInstance;

    [SerializeField]
    private float moveSpeed = 6f;
    private Vector2 move;
    private CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Start()
    {
        playerInputInstance = InputFromPlayer.Instance;

    }
    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        move = playerInputInstance.GetAndReturnPlayerMoveInput();
        Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
        characterController.Move(movement * moveSpeed);
    }

}
