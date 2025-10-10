using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    [SerializeField]private float _health;

    private void Start()
    {
        if(_health==0)
        {
            _health=1;
        }
    }

    
    public void Hit(float damage)
    {
        _health -= damage;
    }
    public float GetHealth()
    {
        return _health;
    }
}
