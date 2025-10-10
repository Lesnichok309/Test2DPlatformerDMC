using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    [SerializeField] private Menu _menu;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _menu != null)
        {
            _menu.NextLevel();
        }
        else
        {
            if (_menu == null)
                Debug.LogError("Forgot add menu"); 
        }
    }
}
