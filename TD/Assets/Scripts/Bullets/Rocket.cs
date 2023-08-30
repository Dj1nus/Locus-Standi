using System.Collections;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private Explosion _explosion;
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private float _smokeSpawnRate;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed = 95f;
    [SerializeField] private float _directHitDamage;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _timeUntilLaunch;
    
    private Quaternion _tmpRotation;
    private int _damage;
    private Entity _target;
    private bool _isLaunched = false;
    private Rigidbody _rb;
    private Vector3 _rememberPosition = new Vector3(50, 40, 45);

    public void Init(int damage, Entity target)
    {
        _rb = GetComponent<Rigidbody>();

        _damage = damage;
        _target = target;

        StartCoroutine(UntilLaunchTimer());
        StartCoroutine(SmokeSpawner());

        transform.forward = Vector3.up;
    }

    IEnumerator SmokeSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(_smokeSpawnRate);
            Instantiate(_smoke, transform.position, Quaternion.identity);
        }
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    IEnumerator UntilLaunchTimer()
    {
        yield return new WaitForSeconds(_timeUntilLaunch);
        _isLaunched = true;

        StartCoroutine(LifeTimer());
    }

    private void OnTriggerEnter(Collider other)
    {
        _isLaunched = false;
        StopCoroutine(SmokeSpawner());

        if (other.TryGetComponent(out EnemyEntity target))
        {
            target.TakeDamage(_directHitDamage);
        }

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        Collider[] overlapedColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider col in overlapedColliders)
        {
            if (col.TryGetComponent(out EnemyEntity enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }

        Instantiate(_explosion, transform.position, Quaternion.identity).Explode();

        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (_isLaunched)
        {
            _rb.velocity = transform.forward * _speed;

            if (_target != null)
            {
                _rememberPosition = _target.transform.position - transform.position;
            }

            _tmpRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_rememberPosition), _rotateSpeed * Time.deltaTime);
            transform.rotation = _tmpRotation;
        }
    }
}
