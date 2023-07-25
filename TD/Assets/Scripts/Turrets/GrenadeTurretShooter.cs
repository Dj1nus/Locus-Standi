using UnityEngine;

public class GrenadeTurretShooter : TurretShooter
{
    public override void Shoot(Entity target)
    {
        base.Shoot(target);

        Vector3 direction = target.transform.position - _muzzle.position;

        var newBullet = _bulletPool.CreateBullet();

        newBullet.GetComponent<Bullet>().Init(_damagePerBullet);
        newBullet.transform.position = _muzzle.position;

        newBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * _shootForce, ForceMode.Impulse);

        StartCoroutine(CooldownTimer());
    }
}
