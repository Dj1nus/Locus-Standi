using System.Collections;
using UnityEngine;

public class Grenade : Explosion
{
    [SerializeField] private float _lifeTime;

    [SerializeField] private float _explosionRadius;

    private float _damage;

    public void Init(float damage)
    {
        _damage = damage;
        StartCoroutine(LifeTimer());
    }

    private IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(_lifeTime);

        Explode(_explosionRadius, _damage);
    }
}
