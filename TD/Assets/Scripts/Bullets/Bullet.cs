using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        if (other.TryGetComponent(out EnemyEntity enemy))
        {
            StopCoroutine(LifeTimer());
            enemy.TakeDamage(_damage);
            Deactivate();
        }
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(_liveTime);
        Deactivate();
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = new Vector3(50, 5, 50);
    }
}
