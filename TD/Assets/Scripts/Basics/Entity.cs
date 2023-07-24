using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _maxHp;
    [SerializeField] private Cost _moneysForKilling;
    [SerializeField] private float _distanceToDamage;
    [SerializeField] private float _attackCooldown;

    private bool _isCanAttack = true;
    private HealthBar _healthBar;
    [SerializeField] private float _hp;
    private AudioPlayer _audioPlayer;

    public float DistanceToDamage { get { return _distanceToDamage; } }

    public Cost GetMoneyForKilling()
    {
        return _moneysForKilling;
    }

    public float GetHp()
    {
        return _hp;
    }

    public float GetDamage ()
    {
        return _damage;
    }

    public void TakeDamage(float value)
    {
        _hp -= value;

        if (_audioPlayer != null)
        {
            _audioPlayer.Play("Hit", Random.Range(0.5f, 1.5f));
        }

        CheckHp();
    }

    public void Attack(Entity target)
    {
        if (_isCanAttack)
        {
            StartCoroutine(AttackCooldown());
            target.TakeDamage(_damage);
        }
    }

    IEnumerator AttackCooldown()
    {
        _isCanAttack = false;
        yield return new WaitForSeconds(_attackCooldown);
        _isCanAttack = true;
    }

    protected virtual void Die()
    {
        if (_audioPlayer != null)
        {
            _audioPlayer.Play("Die");
        }

        Destroy(gameObject);
    }

    public void CheckHp()
    {
        if (_hp <= 0)
        {
            Die();
        }

        _healthBar.UpdateHealthBat(_maxHp, _hp);
    }

    private void Start()
    {
        _hp = _maxHp;

        _healthBar = GetComponentInChildren<HealthBar>();
        _healthBar.UpdateHealthBat(_maxHp, _hp);

        _audioPlayer = GetComponentInChildren<AudioPlayer>();
    }
}
