using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamStateMachine : BaseEnemyStateMachine
{
    //IEnumerator LifeTimer()
    //{
    //    yield return new WaitForSeconds(20);
    //    Destroy(gameObject);
    //}

    //void Start()
    //{
    //    StartCoroutine(LifeTimer());
    //    Init();
    //}

    private void Update()
    {
        StateMachine();
    }
}
