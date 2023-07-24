using System.Collections;
using UnityEngine;


public class ShotgunTurretShooter : TurretShooter
{
    [SerializeField] private int _bulletsPerShot;
    [SerializeField] private float _delayBetweenShots;

    private AudioPlayer _audioPlayer;

    public override void Shoot(Entity target)
    {
        StartCoroutine(DelayBetweenBullets(target));
    }

    IEnumerator DelayBetweenBullets(Entity target)
    {
        _state = states._shooting;
        Vector3 direction = target.transform.position - _muzzle.position;
        
        for (int i = 0; i < _bulletsPerShot; i++)
        {
            var newBullet = _bulletPool.CreateBullet();

            newBullet.GetComponent<Bullet>().Init(_damagePerBullet);
            newBullet.transform.position = _muzzle.position;

            newBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * _shootForce, ForceMode.Impulse);

            _audioPlayer.Play("Attack", Random.Range(0.9f, 1.1f));

            yield return new WaitForSeconds(_delayBetweenShots);
        }

        StartCoroutine(CooldownTimer());
    }

    private void Awake()
    {
        _audioPlayer = GetComponentInChildren<AudioPlayer>();
    }
}


