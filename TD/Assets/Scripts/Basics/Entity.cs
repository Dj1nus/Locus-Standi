using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{
    public UnityEvent<Entity> iAmDied = new UnityEvent<Entity>();

    public static Action OnBaseDestroyed;

    [SerializeField] private float _damage;
    [SerializeField] private float _hp;
    [SerializeField] private float _distanceToDamage;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private bool _isMainBase;
    [SerializeField] private bool _isEnemy;

    private bool _isCanAttack = true;
    
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

    public virtual void Die()
    {
        if (_isMainBase)
        {
            OnBaseDestroyed?.Invoke();
        }

        else if (_isEnemy) 
        {
            iAmDied?.Invoke(GetComponent<Entity>());
        }
        
        Destroy(gameObject);

        //gameObject.SetActive(false);
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
