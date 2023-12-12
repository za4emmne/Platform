using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInstantiating : MonoBehaviour
{
    [SerializeField] private Coin _coin;
    [SerializeField] private float _minWaitSecondInstantiateCoin;
    [SerializeField] private float _maxWaitSecondInstantiateCoin;

    private int _countStartCoins;

    private void Start()
    {
        Instantiate(_coin, transform.position, Quaternion.identity);
        _countStartCoins = FindObjectsOfType<Coin>().Length;
    }

    private void Update()
    {
        //int CountCoins = FindObjectsOfType<Coin>().Length;

        //if (CountCoins<_countStartCoins)
        //{
        //    Instantiate(_coin, transform.position, Quaternion.identity);
        //}
    }
}
