using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    private float _liveTime = 10;

    private void OnTriggerEnter(Collider other)
    {
        print(other.name);
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
    }

    private void Start()
    {
        StartCoroutine(LifeTimer());
    }
}
