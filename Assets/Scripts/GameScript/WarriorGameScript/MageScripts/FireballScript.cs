using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    [SerializeField] private float _speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right*_speed*Time.deltaTime);
        
    }

    public void AttakThis(GameObject Target)
    {
        Vector2 Direction = Target.transform.position - transform.position;
        float Angle = Vector2.SignedAngle(Vector2.up, Direction);
        transform.eulerAngles = new Vector3(0, 0, Angle+90);
    }
}
