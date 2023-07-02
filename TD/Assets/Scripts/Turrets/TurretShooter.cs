using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _sphere;
    [SerializeField] private GameObject _muzzleBox;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private float _damage;
    [SerializeField] private float _shootForce;
    [SerializeField] private float _fireRate;

    private bool _isCanShoot = true;
    private Transform _headTransform;
    private Vector3 vectorOfLook;

    public void Shoot(Entity target)
    {
        if (target == null) return;

        _sphere.transform.LookAt(new Vector3(
            target.transform.position.x,
            _sphere.transform.position.y,
            target.transform.position.z));

        _muzzleBox.transform.LookAt(new Vector3(
            _muzzleBox.transform.position.x,
            target.transform.position.y,
            _muzzleBox.transform.position.z));
        
        //_head.transform.LookAt(target.transform.position);
        //_head.transform.LookAt(target.transform.position);

        

        if (_isCanShoot)
        {
            var newBullet = Instantiate(_bullet);

            newBullet.GetComponent<Bullet>().Init(_damage);
            newBullet.transform.position = _muzzle.position;

            Vector3 direction = target.transform.position - _muzzle.position;

            newBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * _shootForce, ForceMode.Impulse);

            StartCoroutine(CooldownTimer());
        }
    }

    IEnumerator CooldownTimer()
    {
        _isCanShoot = false;
        yield return new WaitForSeconds(_fireRate);
        _isCanShoot = true;
    }
}
