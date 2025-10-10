using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamageScript : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] bool _destroyWeapon;
    [SerializeField] float _destroyTime;

    void Start()
    {
        if (_destroyWeapon)
        {
            Destroy(gameObject, _destroyTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            if (_destroyWeapon) Destroy(gameObject);
        }

        if (collision.gameObject != gameObject && collision.isTrigger == false)
        {
            HealthScript targetHealth;
            if (collision.gameObject.TryGetComponent<HealthScript>(out targetHealth))
            {
                targetHealth.Hit(_damage);

                if (_destroyWeapon) Destroy(gameObject);
            }
        }
    }
}
