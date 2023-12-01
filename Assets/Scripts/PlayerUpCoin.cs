using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerUpCoin : MonoBehaviour
{
    [SerializeField] private UnityEvent _getCoin;
    [SerializeField] private float _waitSecondToDestoryCoin = 0.3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            _getCoin.Invoke();
            StartCoroutine(DestroyCoin(coin.gameObject));
        }
    }

    private IEnumerator DestroyCoin(GameObject coin)
    {
        var waitForSecond = new WaitForSeconds(_waitSecondToDestoryCoin);
        yield return waitForSecond;
        Destroy(coin);
    }
}