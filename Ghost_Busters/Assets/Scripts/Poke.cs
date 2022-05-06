using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Poke : MonoBehaviour
{
    [SerializeField] private float force;

    private PlayerControls playerControls;
    private InputAction poke, pull;
    private RaycastHit hit;

    private void Awake()
    {
        playerControls = new PlayerControls();

        poke = playerControls.Player.Poke;
        pull = playerControls.Player.Pull;

        poke.performed += OnPoke;
        pull.performed += OnPull;
    }

    private void OnPull(InputAction.CallbackContext obj)
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit) && hit.collider.tag == "PhysObject")
        {
            hit.collider.GetComponent<Rigidbody>().AddForce(-transform.forward * force, ForceMode.Impulse);
        }
    }

    private void OnPoke(InputAction.CallbackContext obj)
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit) && hit.collider.gameObject.GetComponent<Possessable>())
        {
            hit.collider.gameObject.GetComponent<Possessable>().Possess();
        }

        Debug.Log(hit.collider);
    }

    private void OnEnable()
    {
        poke.Enable();
        pull.Enable();
    }

    private void OnDisable()
    {
        poke.Disable();
        pull.Disable();
    }


}
