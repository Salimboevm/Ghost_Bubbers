using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerVisionController : MonoBehaviour
{
    InputFromPlayer playerInputInstance;

    [SerializeField]
    private float mouseSensitivity = 100f;
    private Vector2 mouseLook;
    private float xRotation = 0f;
    [SerializeField]
    private Transform playerBody;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }
    private void Start()
    {
        playerInputInstance = InputFromPlayer.Instance;
    }
    private void Update()
    {
        RotatePlayer();
    }
    /// <summary>
    /// getting player looking rotation side by mouse position
    /// </summary>
    private void RotatePlayer()
    {
        mouseLook = playerInputInstance.GetPlayerLookAroundInput();//connect mouse input with variable

        float mouseX = mouseLook.x * mouseSensitivity * Time.deltaTime;//get mouse x rotation

        transform.localRotation = Quaternion.Euler(CalculateXRotation(), 0, 0);//rotate camera
        playerBody.Rotate(Vector3.up * mouseX);//rotate player
    }
    private float CalculateXRotation()
    {
        float mouseY = mouseLook.y * mouseSensitivity * Time.deltaTime;//get mouse y rotations

        xRotation -= mouseY;//get vertical rotation
        xRotation = Mathf.Clamp(xRotation, -90, 90);//clamping vertical rotation angle
        return xRotation;
    }

}
