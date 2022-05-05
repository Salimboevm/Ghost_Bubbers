using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalObject : Possessable
{
    [SerializeField] private float bounceInterval, bounceForce;

    private Rigidbody rb;
    private Vector3 bounceDirection;
    private bool possessed = false;
    private float timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Possess()
    {
        possessed = true;
    }

    public override void Unossess()
    {
        possessed = false;
    }

    private void Update()
    {
        if (possessed)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timer = bounceInterval;

                bounceDirection = Vector3.up;
                bounceDirection = Quaternion.Euler(Random.Range(-45, 45), Random.Range(0, 360), 0) * bounceDirection;

                rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
            }
        }
    }
}