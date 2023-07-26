using UnityEngine;

public class GrenadeTurretShooter : TurretShooter
{
//    [SerializeField] protected GameObject _grenade;

    public override void Shoot(Entity target)
    {
        base.Shoot(target);

        Vector3 direction = target.transform.position - _muzzle.position;

        var newBullet = Instantiate(_bullet);

        newBullet.GetComponent<Grenade>().Init(_damagePerBullet);
        newBullet.transform.position = _muzzle.position;

        newBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * _shootForce, ForceMode.Impulse);

        StartCoroutine(CooldownTimer());
    }
}
