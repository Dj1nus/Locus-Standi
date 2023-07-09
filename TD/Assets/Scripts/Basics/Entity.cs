using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public static Action OnBaseDestroyed;

    [SerializeField] private float _damage;
    [SerializeField] private float _hp;
    [SerializeField] private Cost _moneysForKilling;
    [SerializeField] private float _distanceToDamage;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private bool _isMainBase;
    [SerializeField] public bool _isEnemy;

    private bool _isCanAttack = true;
    private bool _isSignalSended = false;

    public float DistanceToDamage { get { return _distanceToDamage; } }

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
        if (!_isSignalSended)
        {
            _isSignalSended = true;
            GlobalEventManager.SendBuildingDestroy(GetComponent<MapUnit>());
        }
        
        Destroy(gameObject);
    }

    public void CheckHp()
    {
        if (_hp <= 0)
        {
            Die();
        }
    }
}
