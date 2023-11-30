using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string AnimationRun = "HorizontalMove";
    private const string AnimationJump = "Jumping";
    private const string AnimationDangeon = "Dangeon";

    private Player _playerLink;
    private Animator _animator;

    void Start()
    {
        _playerLink = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat(AnimationRun, Mathf.Abs(_playerLink.GetHorizontalMove()));

        if (_playerLink.GetiIsGround() == true)
        {
            _animator.SetBool(AnimationJump, false);
        }
        else
        {
            _animator.SetBool(AnimationJump, true);
        }

        if(_playerLink.GetIsEnemy() == true)
        {
            _animator.SetTrigger(AnimationDangeon);
        }
    }
}
