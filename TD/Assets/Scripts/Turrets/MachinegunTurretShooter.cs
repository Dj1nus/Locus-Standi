using UnityEngine;

public class MachinegunTurretShooter : TurretShooter
{
    private AudioPlayer _audioPlayer;

    public override void Shoot(Entity target)
    { 
        base.Shoot(target);

        Vector3 direction = target.transform.position - _muzzle.position;

        var newBullet = _bulletPool.CreateBullet();

        newBullet.GetComponent<Bullet>().Init(_damagePerBullet);
        newBullet.transform.position = _muzzle.position;

        newBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * _shootForce, ForceMode.Impulse);

        _audioPlayer.Play("Attack", Random.Range(0.9f, 1.1f));

        StartCoroutine(CooldownTimer());
    }

    private void Awake()
    {
        _audioPlayer = GetComponentInChildren<AudioPlayer>();
    }
}
