using System.Collections;
using UnityEngine;

public class EnemieShooter : MonoBehaviour
{
    [SerializeField] protected Transform _muzzle;
    [SerializeField] protected float _shootForce;
    [SerializeField] protected EnemyBullet _bullet;

    protected bool _isCanAttack = true;
    private AudioPlayer _audioPlayer;

    public virtual void Shoot(Entity target, float damage, float cooldown)
    {
        if (_audioPlayer != null)
        {
            _audioPlayer.Play(SoundTypes.Attack, Random.Range(0.9f, 1.1f));
        }
    }

    protected IEnumerator CooldownTimer(float time)
    {
        _isCanAttack = false;
        yield return new WaitForSeconds(time);
        _isCanAttack = true;
    }

    private void Start()
    {
        _audioPlayer = GetComponent<AudioPlayer>();
    }

}
