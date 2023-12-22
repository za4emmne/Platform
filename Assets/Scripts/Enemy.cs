using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const string AnimationAttacked = "Attack";

    [SerializeField] private Transform _path;
    [SerializeField] private Transform[] _points;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _player;

    private Animator _animator;
    private int _currentPoint;
    private float _acceleration = 1;
    private int _layerMask = 1 << 6;
    private float _distance = 100f;
    private bool _isPlayerTrigger = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _points = new Transform[_path.childCount];

        for (int i = 0; i < _path.childCount; i++)
        {
            _points[i] = _path.GetChild(i);
        }
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * GetLocalScale(), _distance, _layerMask);
        Debug.DrawRay(transform.position, transform.right* GetLocalScale(), Color.red); 

        if(hit)
        {
            _isPlayerTrigger = true;
        }
        else
        {
            _isPlayerTrigger = false;
        }

        if (_isPlayerTrigger == false)
        {
            Patroling();
        }
        else
        {
            PursuePlayer();
        }       
    }

    private void PursuePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, _player.position, (_speed + _acceleration) * Time.deltaTime);
    }

    private void Flip()
    {
        Vector3 Scale = transform.localScale;
        Scale.x *= -1;
        transform.localScale = Scale;
    }

    private int GetLocalScale()
    {
        int localScale = 1;

        if(transform.localScale.x > 0 )
        {
            return localScale = 1;
        }
        else if(transform.localScale.x < 0)
        {
            return localScale = -1;
        }

        return localScale;
    }

    private void Patroling()
    {
        Transform target = _points[_currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _currentPoint++;

            if (_currentPoint >= _points.Length)
            {
                _currentPoint = 0;
            }
        }

        if (_currentPoint == 0 && transform.localScale.x > 0)
        {
            Flip();
        }
        else if (_currentPoint == 1 && transform.localScale.x < 0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.TryGetComponent(out Player player))
        {
            _animator.SetTrigger(AnimationAttacked);
            
            if(player.GetIsAttacked() == true) 
            {
                Destroy(this.gameObject);
            }
        }
    }
}
