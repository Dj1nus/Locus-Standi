using UnityEngine;

public class RocketTurretShooter : TurretShooter
{
    public override void Shoot(Entity target)
    {
        base.Shoot(target);

        var newRocket = Instantiate(_bullet);

        newRocket.GetComponentInChildren<Rocket>().Init((int)_damagePerBullet, target);
        newRocket.transform.position = _muzzle.position;

        newRocket.GetComponentInChildren<Rigidbody>().AddForce(Vector3.up * _shootForce, ForceMode.Impulse);

        StartCoroutine(CooldownTimer());
    }
}
