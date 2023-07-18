using UnityEngine;

public class MachinegunTurretShooter : TurretShooter
{
    public override void Shoot(Entity target)
    { 
        Vector3 direction = target.transform.position - _muzzle.position;

        var newBullet = _bulletPool.CreateBullet();

        newBullet.GetComponent<Bullet>().Init(_damagePerBullet);
        newBullet.transform.position = _muzzle.position;

        newBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * _shootForce, ForceMode.Impulse);

        StartCoroutine(CooldownTimer());
    }
}
