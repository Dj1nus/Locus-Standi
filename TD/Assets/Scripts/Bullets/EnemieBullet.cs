using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieBullet : MonoBehaviour
{
    private float _damage;
    private float _liveTime = 3f;

    public void Init(float damage)
    {
        _damage = damage;
        StartCoroutine(LifeTimer());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out TurretEntity target))
        {
            StopCoroutine(LifeTimer());
            target.TakeDamage(_damage);
            Destroy(gameObject);
        }

        else if (other.TryGetComponent(out MainBase mainBase))
        {
            StopCoroutine(LifeTimer());
            mainBase.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(_liveTime);
        Destroy(gameObject);
    }
}
