using UnityEngine;
using System;
public class Health : MonoBehaviour
{
    [SerializeField]
    private float _health;
    private bool _isDead;
    public Action<float> _dealDamageAction;
    private Action _deadAction;
    private float HealthValue {
        
        set 
        { 
            _health = value;
            _dealDamageAction(_health);
        } 
    }
    private bool IsDead
    {
        set
        {
            _isDead = value;
            _deadAction();
        }
    }
    private void Start()
    {
        _dealDamageAction += DealDamage;
        _deadAction += CheckIsDead;
    }
    public void DealDamage(float damageValue)
    {
        _health -= damageValue;
        CheckHealth();
        
    }
    void CheckHealth() 
    {
        if (_health <= 0)
        {
            IsDead = true;
        }
    }
    void CheckIsDead()
    {
        print("hed");
        if (_isDead)
        {
            Destroy(gameObject);
        }
    }
}
