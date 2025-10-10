using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField] private Transform GroundCheckL;
    [SerializeField] private Transform GroundCheckR;

    [SerializeField] private PlayerAudio _playerAudio;

    [SerializeField] private float _speed = 5;
    [SerializeField] private float _jumpPower = 5;
    private float _lastHealth;
   
    private HealthScript _myHealth;
    private Animator _animator;
    private Rigidbody2D _rb;
    private bool _attakFlag;
   
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _myHealth = GetComponent<HealthScript>();
        _lastHealth = _myHealth.GetHealth();
    }

   
    void Update()
    {
        HitDieCheck();
        GroundCheck();
        Move();

        if (Input.GetKeyDown(KeyCode.Space) && !_animator.GetBool("Jump") && !_attakFlag)
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            _playerAudio.PlayAudio(PlayerAudio.AudioType.Jump,true);
            _animator.SetBool("Jump", true);
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0) && !_animator.GetBool("Jump") && !_attakFlag)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y);
            _animator.SetTrigger("Attak");
            _playerAudio.PlayAudio(PlayerAudio.AudioType.Attak);
            _attakFlag = true;
            Invoke("StopAttak", 0.533f); // Время анимации атаки, она одна потому решил простым способом. 
        }
    }
    private void GroundCheck()
    {
        RaycastHit2D HitGroundL = Physics2D.Raycast(GroundCheckL.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        RaycastHit2D HitGroundR = Physics2D.Raycast(GroundCheckR.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        if (HitGroundL.collider != null || HitGroundR.collider != null)
        {
            _animator.SetBool("Jump", false);
        }
        else
        {
            _animator.SetBool("Jump", true);
        }
    }
    private void HitDieCheck()
    {
       if(_myHealth.GetHealth()!=_lastHealth)
        {
            if (_myHealth.GetHealth() <= 0)
            {
                _animator.SetTrigger("Die");
                gameObject.layer = LayerMask.NameToLayer("AFK");
                GetComponent<AnimationScript>().enabled = false;
            }
            else
            {
                _playerAudio.PlayAudio(PlayerAudio.AudioType.Hit,true);
                _animator.SetTrigger("Hit");
                _lastHealth = _myHealth.GetHealth();
            }
        }
    }
    private void StopAttak()
    {
      _attakFlag = false;       
    }
    private void Move()
    {
        float moveAxis = Input.GetAxis("Horizontal");
        if (moveAxis!=0 && !_attakFlag)
        {
            _animator.SetBool("Move", true);
            if (moveAxis > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _rb.velocity = new Vector2(_speed, _rb.velocity.y);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                _rb.velocity = new Vector2(-_speed, _rb.velocity.y);
            }
        }
        else
        {
            _animator.SetBool("Move", false);
            _rb.velocity = new Vector2(_rb.velocity.x * 0.9f, _rb.velocity.y);
        }
    }

  
}
