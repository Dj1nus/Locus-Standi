using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _maxHp;
    [SerializeField] private Cost _moneysForKilling;
    [SerializeField] private float _distanceToDamage;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private Explosion _deathEffect;

    [SerializeField] private float _dieVolume;
    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private DeathAudioPlayer _audioPlayer;

    private HealthBar _healthBar;
    private float _hp;

    protected bool IsCanAttack = true;
    protected AudioPlayer AudioPlayer;
    protected bool IsSignalSended = false;

    public float GetDistanceToDamage() => _distanceToDamage;

    public float GetCoolDown() => _attackCooldown;

    public Cost GetMoneyForKilling() => _moneysForKilling;

    public float GetHp() => _hp;

    public float GetDamage() => _damage;

    private void Start()
    {
        _hp = _maxHp;

        _healthBar = GetComponentInChildren<HealthBar>();
        _healthBar.UpdateHealthBat(_maxHp, _hp);

        AudioPlayer = GetComponentInChildren<AudioPlayer>();
    }

    public void TakeDamage(float value)
    {
        _hp -= value;

        if (AudioPlayer != null)
        {
            AudioPlayer.Play(SoundTypes.Hit, Random.Range(0.5f, 1.5f));
        }

        CheckHp();
    }
    public void CheckHp()
    {
        if (_hp <= 0)
        {
            Die();
        }

        _healthBar.UpdateHealthBat(_maxHp, _hp);
    }

    public virtual void Attack(Entity target)
    {
        if (IsCanAttack)
        {
            StartCoroutine(AttackCooldown());
            target.TakeDamage(_damage);
        }
    }

    protected virtual void Die()
    {
        if (_deathEffect != null)
        {
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
        }

        if (_deathClip)
        {
            DeathSound sound = new(_deathClip, _dieVolume, Random.Range(0.9f, 1.1f), 1f, 4f);

            Instantiate(_audioPlayer, transform.position, Quaternion.identity).Init(sound);
        }

        Destroy(gameObject);
    }
    IEnumerator AttackCooldown()
    {
        IsCanAttack = false;
        yield return new WaitForSeconds(_attackCooldown);
        IsCanAttack = true;
    }
}
