using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _jumpForce = 8f;
    [SerializeField] private float _dangeonForce = 1f;

    private Rigidbody2D _rigidbody2D;
    private float _horizontalMove = 0f;
    private bool _isFaceRight = true;
    private bool _isGround;
    private bool _isEnemy;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(_horizontalMove * 5f, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = targetVelocity;
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
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent(out Enemy enemy))
        {
            _isEnemy = true;
            _rigidbody2D.AddForce(transform.right * _dangeonForce, ForceMode2D.Impulse);
        }
        else
        {
            _isEnemy = false;
        }
    }
}