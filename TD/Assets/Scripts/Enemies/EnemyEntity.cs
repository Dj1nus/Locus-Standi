using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : Entity
{
    public Action<EnemyEntity> EnemyDied;

    protected override void Die()
    {
        EnemyDied?.Invoke(GetComponent<EnemyEntity>());
    }
}
