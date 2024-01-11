using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _jumpForce = 8f;    
    [SerializeField] private float _health;
    [SerializeField] private int _damage = 1;
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _enemyLayer;

    private Rigidbody2D _rigidbody2D;
    private float _horizontalMove = 0f;
    private bool _isFaceRight = true;
    private bool _isGround;
    private bool _isEnemy;
    private bool _isAttacked;
    private float _dangeonForce;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _health = 5;
    }

    private void Update()
    {
        //Debug.Log("HEALTH: " + _health);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && _isGround)
        {
            _rigidbody2D.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
        }

        _horizontalMove = Input.GetAxisRaw("Horizontal") * _speed;

        if (_horizontalMove < 0 && _isFaceRight)
        {
            Flip();
        }
        else if (_horizontalMove > 0 && !_isFaceRight)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            _isAttacked = true;
            Attacked();
        }
        else
        {
            _isAttacked = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(_horizontalMove * 5f, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = targetVelocity;
    }

    private void Attacked()
    { 
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);

        foreach (var enemy in hitEnemy)
        {
            enemy.GetComponent<Enemy>().TakeDamage(_damage);
        }
    }

    public void ChangeHealth(int changer)
    {
        _health += changer;
    }

    public float GetHorizontalMove()
    {
        return _horizontalMove;
    }

    public bool GetiIsGround()
    {
        return _isGround;
    }

    public bool GetIsEnemy()
    {
        return _isEnemy;
    }

    public bool GetIsAttacked()
    {
        return _isAttacked;
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    private void Flip()
    {
        _isFaceRight = !_isFaceRight;

        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Flour flour))
        {
            _isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Flour flour))
        {
            _isGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //столкновение с врагом,
                                                           //получение урона
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            _isEnemy = true;
            _health--;
        }
        else
        {
            _isEnemy = false;
        }
    }
}