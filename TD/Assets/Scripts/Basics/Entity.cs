using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _maxHp;
    [SerializeField] private Cost _moneysForKilling;
    [SerializeField] private float _distanceToDamage;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private ParticleSystem _deathEffect;

    private HealthBar _healthBar;
    private float _hp;

    protected bool IsCanAttack = true;
    protected AudioPlayer AudioPlayer;
    protected bool IsSignalSended = false;

    public float GetDistanceToDamage() => _distanceToDamage;// { get { return _distanceToDamage; } }

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
            AudioPlayer.Play("Hit", Random.Range(0.5f, 1.5f));
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
            Instantiate(_deathEffect, transform);
            _deathEffect.Play();
        }

        if (AudioPlayer != null)
        {
            //AudioPlayer.Play("Die");
            StartCoroutine(DieSoundDelay());
        }
        else
        {
            Destroy(gameObject);
        }
    }
    IEnumerator AttackCooldown()
    {
        IsCanAttack = false;
        yield return new WaitForSeconds(_attackCooldown);
        IsCanAttack = true;
    }

    IEnumerator DieSoundDelay()
    {
        AudioPlayer.Play("Die");
        yield return new WaitForSeconds(AudioPlayer.GetSoundDuration("Die"));
        Destroy(gameObject);
    }
}
