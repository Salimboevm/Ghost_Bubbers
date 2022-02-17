using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [SerializeField] private Transform camera;
    [SerializeField] private float speed, sensitivity;

    private Rigidbody rb;
    private PlayerControls playerControls;
    private InputAction move, look;

    private void Awake()
    {
        playerControls = new PlayerControls();

        move = playerControls.Player.Move;
        look = playerControls.Player.Look;
    }

    private void OnEnable()
    {
        move.Enable();
        look.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        look.Disable();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.eulerAngles.y + look.ReadValue<Vector2>().x * sensitivity * Time.deltaTime, 0));
        camera.localRotation = Quaternion.Euler(new Vector3(camera.localEulerAngles.x - look.ReadValue<Vector2>().y * sensitivity * Time.deltaTime, 0, 0));
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + (transform.right * move.ReadValue<Vector2>().x + transform.forward * move.ReadValue<Vector2>().y) * speed * Time.deltaTime);
    }
}
