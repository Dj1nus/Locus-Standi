using System;

public class EnemyEntity : Entity
{
    public Action<EnemyEntity> EnemyDied;

    protected override void Die()
    {
        EnemyDied?.Invoke(GetComponent<EnemyEntity>());
        GlobalEventManager.SendEnemyDied(GetMoneyForKilling());

        base.Die();
    }
}
