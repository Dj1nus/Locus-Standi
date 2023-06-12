using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _hp;
    [SerializeField] private float _distanceToDamage;
    [SerializeField] private float _attackCooldown;

    private bool _isCanAttack = true;

    public float GetHp()
    {
        return _hp;
    }

    public float GetDamageValue()
    {
        return _damage;
    }

    public void GetDamage(float value)
    {
        _hp -= value;
    }

    public void Attack(Entity target)
    {
        if (_isCanAttack)
        {
            StartCoroutine(AttackCooldown());
            target.GetDamage(_damage);
        }
    }

    IEnumerator AttackCooldown()
    {
        _isCanAttack = false;
        yield return new WaitForSeconds(_attackCooldown);
        _isCanAttack = true;
    }

    public void Die()
    {
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
