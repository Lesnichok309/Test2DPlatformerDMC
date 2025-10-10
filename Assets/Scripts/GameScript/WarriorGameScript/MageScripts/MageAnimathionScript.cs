using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageAnimathionScript : MonoBehaviour
{
    [SerializeField] private LayerMask visionMask;
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private Transform weaponPoint;

    [SerializeField] private float moveSpeed = 1;

    [SerializeField] private float shootPause = 1;
    private Animator _animator;
    private HealthScript _myHealth;
    private Rigidbody2D _rb;

    

    private bool directionRigth = true;
    private bool hunting;
    private float _defaultSpeed;
    private float _lastHealth;
    
    private bool _shootFlag;

    void Start()
    {
        _myHealth = GetComponent<HealthScript>();
        _lastHealth = _myHealth.GetHealth();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _defaultSpeed = moveSpeed;

        if (transform.rotation.eulerAngles == new Vector3(0, 180, 0))
        {
            ChangeDirection();
        }
    }

    void FixedUpdate()
    {
        HitDieCheck();
        if (!hunting)
        {
            _rb.velocity = new Vector2(moveSpeed / 2, _rb.velocity.y);
        }        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector3 TrigPosition = collision.gameObject.transform.position;
        
        if (collision.gameObject.CompareTag("Player") && !_animator.GetBool("Die"))
        {
            hunting = true;
            if (transform.position.x <= TrigPosition.x)
            {
                ChangeDirection(true,false);
            }
            else
            {
                ChangeDirection(false,true);
            }

            Debug.DrawLine(transform.position, TrigPosition, Color.red);
            
            if (Physics2D.Linecast(transform.position, TrigPosition, visionMask)) // проверяет не за препятствием ли игрок
            {
                _rb.velocity = new Vector2(moveSpeed / 2, _rb.velocity.y);
                _animator.SetBool("Move", true);
            }
            else
            {
                _animator.SetBool("Move", false);
                _rb.velocity = new Vector2(0, _rb.velocity.y);
                if (!_shootFlag)
                {
                    _animator.SetTrigger("AttakMid");
                    StartCoroutine(AttakIE(collision.gameObject));
                }
            }
        }
        else
        {
            _animator.SetBool("Move", false);
        } 
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hunting = false;
        }
    }

    private void HitDieCheck()
    {
        if (_myHealth.GetHealth() != _lastHealth)
        {
            if (_myHealth.GetHealth() <= 0)
            {
                StopAllCoroutines();
                _animator.SetTrigger("DieTriger");
                _animator.SetBool("Die", true);
                gameObject.layer = LayerMask.NameToLayer("AFK");
                _rb.velocity = new Vector2(0, _rb.velocity.y);
                TryGetComponent<EnemyScript>(out EnemyScript myEnemy);
                Destroy(myEnemy);
                enabled = false;
            }
            else
            {
                StopAllCoroutines();
                _animator.SetTrigger("Hit");
                _lastHealth = _myHealth.GetHealth();
                StartCoroutine(HitStopIE());
            }
        }
    }

    public void ChangeDirection(bool rigth = false, bool left = false)
    {
        if (rigth || left)
        { 
            if (rigth) directionRigth = true;
            if (left)  directionRigth = false;
        }
        else directionRigth = !directionRigth;

        if (directionRigth)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            moveSpeed = _defaultSpeed;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            moveSpeed = -_defaultSpeed;
        }
    }
    IEnumerator AttakIE(GameObject Enemy)
    {
        _shootFlag = true;
        yield return new WaitForSeconds(0.7f); // ожидание для совпадения с анимацией колдуна
        GameObject Ball = Instantiate(fireballPrefab,weaponPoint.position, Quaternion.identity);
        Ball.GetComponent<FireballScript>().AttakThis(Enemy);
        yield return new WaitForSeconds(shootPause);
        _shootFlag = false;
    }
    IEnumerator HitStopIE()
    {
        yield return new WaitForSeconds(0.8f); // ожидание окончания анимации удара по колдуну
        _shootFlag = false;
    }
    
}