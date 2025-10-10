using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWall : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger == false)
        {
            
            if (collision.TryGetComponent<MageAnimathionScript>(out MageAnimathionScript mage))
            {
                mage.ChangeDirection();  // надо бы сделать общий скрипт поворотов для всех врагов кто должен уметь. Но здесь только маг.
            }
        }
    }
}
