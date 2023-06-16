using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public static Action OnBaseDestroyed;

    [SerializeField] private float _damage;
    [SerializeField] private float _hp;
    [SerializeField] private float _distanceToDamage;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private bool _isMainBase;

    private bool _isCanAttack = true;


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
        CheckHp();
        _hp -= value;
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

    public virtual void Die()
    {
        if (_isMainBase)
        {
            OnBaseDestroyed?.Invoke();
        }
        
        Destroy(gameObject);
    }

    public void CheckHp()
    {
        if (_hp <= 0)
        {
            //print(gameObject.name);
            //print("is dying");
            Die();
        }
    }
}
