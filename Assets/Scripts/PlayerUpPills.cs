using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpPills : MonoBehaviour
{
    
    [SerializeField] private float _waitSecondToReturnPills;
    [SerializeField] private float _waitSecondToDestoryPills = 0.3f;
    private int _pillsHP = 1;
    private Player _player;

    private void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Pills pills))
        {
            _player.ChangeHealth(_pillsHP);
            StartCoroutine(DestroyPills(pills.gameObject));
        }
    }

    private IEnumerator DestroyPills(GameObject pills)
    {
        float MinSecondToReturn = 3;
        float MaxSecontToReturn = 10;
        _waitSecondToReturnPills = Random.Range(MinSecondToReturn, MaxSecontToReturn);

        var waitForSecond = new WaitForSeconds(_waitSecondToDestoryPills);
        yield return waitForSecond;
        pills.SetActive(false);
        var waitForSecondToReturn = new WaitForSeconds(_waitSecondToReturnPills);
        yield return waitForSecondToReturn;
        pills.SetActive(true);
    }
}
