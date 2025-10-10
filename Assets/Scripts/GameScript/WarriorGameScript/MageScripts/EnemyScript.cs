using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
 
    [SerializeField] float _contactDamage;

    [SerializeField] bool _forceDestroy = true; 
    private HealthScript _myHealth;
    void Start()
    {
        _myHealth = GetComponent<HealthScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_forceDestroy)
        { 
            DieCheck();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthScript>().Hit(_contactDamage);
        }
    }
    private void DieCheck()
    {
        if (_myHealth.GetHealth() <= 0)
        {
            Destroy(gameObject);
        }
    }
}
