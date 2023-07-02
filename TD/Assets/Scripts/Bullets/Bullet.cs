using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class Bullet : MonoBehaviour
{
    private float _damage;
    private float _liveTime = 10;

    public void Init(float damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BaseEnemyStateMachine enemy))
        {
            StopCoroutine(LifeTimer());
            enemy.GetComponent<Entity>().TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(_liveTime);
        Destroy(gameObject);
    }

    private void Start()
    {
        StartCoroutine(LifeTimer());
    }
}
