using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInstantiating : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private float _minWaitSecondInstantiateCoin;
    [SerializeField] private float _maxWaitSecondInstantiateCoin;

    private void Start()
    {
        Instantiate(_coin, transform.position, Quaternion.identity);
    }
}
