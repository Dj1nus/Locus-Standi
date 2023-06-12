using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinterStateMachine : BaseEnemyStateMachine
{
    // Start is called before the first frame update
    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(20);
        Destroy(gameObject);
    }

    void Start()
    {
        StartCoroutine(LifeTimer());
        Init();
    }

    private void Update()
    {
        StateMachine();
    }
}
