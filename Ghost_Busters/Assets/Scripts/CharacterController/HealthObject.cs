using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// For some reason inheritance was not working

public class HealthObject : MonoBehaviour
{
    private float _health = 40;
    public void TakeDamage(float damage) { _health -= damage; }
    private bool _isDead = false;
    private void Update()
    {
        if (_health <= 0)
        {
            _health = 0; 
            if (!_isDead)
            {
                _isDead = true;
                AI_EventsManager._instance.ObjectCleared(GetComponent<Possessable>()._id); 
            }
        }
    }
}
