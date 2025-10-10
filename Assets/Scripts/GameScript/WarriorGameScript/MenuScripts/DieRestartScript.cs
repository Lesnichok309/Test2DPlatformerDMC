using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieRestartScript : MonoBehaviour
{
    [SerializeField] Menu _menu;
    [SerializeField] HealthScript playerHp;

    private Transform playerTr;
    void Start()
    {
        playerTr = playerHp.gameObject.transform;
    }
    void FixedUpdate()
    {
        if (playerHp.GetHealth() <= 0 || playerTr.position.y <= -5)
        {
            Invoke("DieRestart", 2);
        }
    }
    private void DieRestart()
    { 
        _menu.Restart();
    }


}
