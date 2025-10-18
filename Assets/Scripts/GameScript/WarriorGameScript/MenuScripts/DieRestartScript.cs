using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieRestartScript : MonoBehaviour
{
    [SerializeField] Menu _menu;
    [SerializeField] HealthScript playerHp;

    private bool restart = false;
    private Transform playerTr;
    void Start()
    {
        playerTr = playerHp.gameObject.transform;
    }
    void FixedUpdate()
    {
        if (!restart)
        {
            if (playerHp.GetHealth() <= 0 || playerTr.position.y <= -5)
            {
                restart= true;
                Invoke("DieRestart", 2);
            }
        }
        
    }
    private void DieRestart()
    {
        restart = false;
        _menu.Restart();
    }


}
