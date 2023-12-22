using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacked : MonoBehaviour
{
    private Player _playerLink;

    void Start()
    {
        _playerLink = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy) && Attaked())
        {
            _playerLink.GetDamage();
        }
    }

    private bool Attaked()
    {
        if (_playerLink.GetIsAttacked() == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
