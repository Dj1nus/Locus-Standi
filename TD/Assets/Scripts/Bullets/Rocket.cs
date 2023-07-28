using System.Collections;
using UnityEngine;

public class Rocket : Explosion
{
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
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    IEnumerator UntilLaunchTimer()
    {
        yield return new WaitForSeconds(_timeUntilLaunch);
        _isLaunched = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        StopAllCoroutines();

        if (other.TryGetComponent(out EnemyEntity enemy))
        {
            enemy.TakeDamage(_directHitDamage);
        }

        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _rb.rotation = Quaternion.identity;
        transform.position = other.transform.position;
        transform.rotation = Quaternion.identity;

        Explode(_explosionRadius, _damage);
        GetComponent<CapsuleCollider>().enabled = false;
        StartCoroutine(LifeTimer());
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
