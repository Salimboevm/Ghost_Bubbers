using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectilePhysicsShoot : MonoBehaviour
{
    private float moveSpeed = 100f;
    public void Setup(Vector3 shootDirection)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = shootDirection * moveSpeed;
    }
    /// <summary>
    /// check collision
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    /// <summary>
    /// function to solve puzzle 
    /// when collides
    /// </summary>
    void SolvePuzzle()
    {

    }
    /// <summary>
    /// deals damage when collides
    /// </summary>
    void DealDamage()
    {

    }
}
