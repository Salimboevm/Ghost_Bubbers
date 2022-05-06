using UnityEngine;
using System;
public class Health : MonoBehaviour
{
    [SerializeField]
    protected float _health;
    protected bool _isDead;
    public Action<float> _dealDamageAction;
    private Action _deadAction;
    protected float HealthValue {
        
        set 
        { 
            _health = value;
            _dealDamageAction(_health);
        } 
    }
    protected bool IsDead
    {
        set
        {
            _isDead = value;
            _deadAction();
        }
    }
    protected void Start()
    {
        _dealDamageAction += DealDamage;
        _deadAction += CheckIsDead;
    }
    public void DealDamage(float damageValue)
    {
        _health -= damageValue;
        CheckHealth();
        
    }
    protected void CheckHealth() 
    {
        if (_health <= 0)
        {
            IsDead = true;
        }
    }
    protected void CheckIsDead()
    {
        print("Ded");
        if (_isDead)
        {
            Debug.Log("Before Here");
            int aliveCount = 0;
            foreach (AIGhost ghost in AI_SharedInfo._instance.GetGhostList())
            {
                if (ghost != null)
                {
                    aliveCount += 1;
                }
            }

            if (aliveCount == 1)
            {
                PauseMenu._instance.WinGame();
            }

            Destroy(gameObject);
        }
    }
}
