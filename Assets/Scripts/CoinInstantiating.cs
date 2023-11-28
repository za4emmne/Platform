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
        Coin CloneCoin = Instantiate(_coin, transform.position, Quaternion.identity);
    }

    private void Update()
    {
       
    }

    private IEnumerator Instantiate()
    {
        var WaitForSecondInstatiateCoin = Random.Range(_minWaitSecondInstantiateCoin, _maxWaitSecondInstantiateCoin);
        yield return WaitForSecondInstatiateCoin;

        Coin CloneCoin = Instantiate(_coin, transform.position, Quaternion.identity);
    }
}
