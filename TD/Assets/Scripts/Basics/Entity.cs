using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{

    public float _damage;
    public float _hp;

    public float GetHp()
    {
        return _hp;
    }
    public void GetDamage(float value)
    {
        _hp -= value;
        //print(hp);
        //print(value);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    protected void CheckHp()
    {
        if (_hp <= 0)
        {
            print(gameObject.name);
            print("is dying");
            Die();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        print(_hp);
    }
}
