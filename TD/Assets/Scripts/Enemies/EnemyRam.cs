using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRam : BaseEnemyClass
{
    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(LifeTimer());
    }

    void Update()
    {
        StateMachine();
    }
}
