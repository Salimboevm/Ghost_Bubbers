using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectilePhysicsShoot : MonoBehaviour
{
    [SerializeField]
    private float _damageValue;
    [SerializeField]
    private string _ghostTag;
    [SerializeField]
    private string _puzzleTag;

    private void Awake()
    {
        Destroy(gameObject, 10f);
    }

    /// <summary>
    /// check collision
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        var collidingObject = collision.collider;
        //print(collision.collider.tag);
        if(collidingObject.CompareTag("Ghost"))
        {
            DealDamage(collidingObject.GetComponent<Health>(), _damageValue);
        }   
        else if (collidingObject.CompareTag("PossessableObject"))
        {
            collidingObject.GetComponent<HealthObject>().TakeDamage(_damageValue);
        }
        Destroy(gameObject);
    }
    /// <summary>
    /// function to solve puzzle 
    /// when collides
    /// </summary>
    void SolvePuzzle(int id)
    {
        AI_EventsManager._instance.ObjectCleared(id);
    }
    /// <summary>
    /// deals damage when collides
    /// </summary>
    void DealDamage(Health health,float damageValue)
    {
        health._dealDamageAction(damageValue);
    }
}
