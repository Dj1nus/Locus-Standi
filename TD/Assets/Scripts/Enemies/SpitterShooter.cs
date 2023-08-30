using UnityEngine;

public class SpitterShooter : EnemieShooter
{
    public override void Shoot(Entity target, float damage, float cooldown)
    {
        if (_isCanAttack)
        {
            base.Shoot(target, damage, cooldown);

            Vector3 direction = target.transform.position - _muzzle.transform.position;

            EnemyBullet bullet = Instantiate(_bullet, _muzzle.position, Quaternion.identity);
            bullet.Init(damage);

            bullet.GetComponent<Rigidbody>().AddForce(direction.normalized * _shootForce, ForceMode.Impulse);

            StartCoroutine(CooldownTimer(cooldown));
        }
    }
}
