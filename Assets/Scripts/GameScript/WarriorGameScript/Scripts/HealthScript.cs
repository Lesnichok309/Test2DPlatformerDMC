using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHp;

    private void Start()
    {
        if (_maxHp == 0)
        {
            _maxHp = 1;
            Debug.LogError("Forgot to set the MaxHP value"+ name, transform);
        }
        Mathf.Clamp(_health, 0, _maxHp);

        if (_health == 0)
        {
            _health = _maxHp;
        }
        
    }


    public void Hit(float damage)
    {
        if (damage >= 0)
        {
            _health -= damage;
        }
        else
        {
            Debug.LogError("Damage less than zero", transform);
        }
    }

    public float GetHealth()
    {
        return _health;
    }
    public float GetMaxHealth()
    {
        return _maxHp;
    }
}
