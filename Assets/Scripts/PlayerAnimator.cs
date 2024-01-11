using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string AnimationRun = "HorizontalMove";
    private const string AnimationJump = "Jumping";
    private const string AnimationDangeon = "Dangeon";
    private const string AnimationAttacked = "Attacked";
    //private bool _isDangeon = false;

    private Player _playerLink;
    private Animator _animator;

    void Start()
    {
        _playerLink = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        _animator.SetFloat(AnimationRun, Mathf.Abs(_playerLink.GetHorizontalMove()));

        if (_playerLink.GetiIsGround() == false)
        {
            _animator.SetBool(AnimationJump, true);
        }
        else
        {
            _animator.SetBool(AnimationJump, false);
        }

        if(_playerLink.GetIsEnemy() == true)
        {
            _animator.SetTrigger(AnimationDangeon);
        }

        if (_playerLink.GetIsAttacked() == true)
        {
            _animator.SetTrigger(AnimationAttacked);
        }
    }
}
