using System.Collections;
using UnityEngine;

public class TurretShooter : MonoBehaviour
{
    protected enum states
    {
        _rotating,
        _aimed,
        _shooting,
        _reloading
    }

    protected states _state;

    [SerializeField] protected float _damagePerBullet;
    [SerializeField] protected float _shootForce;
    [SerializeField] protected float _rotationSpeed;
    [SerializeField] protected GameObject _bullet;
    [SerializeField] protected Transform _muzzle;

    [SerializeField] private GameObject _sphere;
    [SerializeField] private float _fireRate;

    private AudioPlayer _audioPlayer;
    private Vector3 _lookDirection;
    private Quaternion _lookRotation;
    protected BulletPool _bulletPool;

    public void ShooterStateMachine(Entity target)
    {
        if (target == null) return;

        switch (_state)
        {
            case states._rotating:
                LookAtTarget(target);
                CheckState();
                break;

            case states._aimed:
                Shoot(target);
                break;

            case states._reloading:
                break;

            case states._shooting:
                break;
        }
    }

    private void CheckState()
    {
        if (_state == states._reloading) return;

        if (Mathf.Abs((_lookRotation.eulerAngles - _sphere.transform.rotation.eulerAngles).y) < 1)
        {
            _state = states._aimed;
        }

        else
        {
            _state = states._rotating;
        }
    }

    public virtual void Shoot(Entity target)
    {
        _audioPlayer.PlayShootSound();

        print(0);
    }
   
    private void LookAtTarget(Entity target)
    {
        _lookDirection = target.transform.position - transform.position;
        _lookRotation = Quaternion.LookRotation(_lookDirection);
        _sphere.transform.rotation = Quaternion.RotateTowards(_sphere.transform.rotation, _lookRotation, _rotationSpeed);
    }

    protected IEnumerator CooldownTimer()
    {
        _state = states._reloading;
        yield return new WaitForSeconds(_fireRate);
        _state = states._rotating;
    }

    private void Start()
    {
        _bulletPool = FindObjectOfType<BulletPool>().GetComponent<BulletPool>();
        _audioPlayer = GetComponent<AudioPlayer>();
        _state = states._rotating;
    }
}
