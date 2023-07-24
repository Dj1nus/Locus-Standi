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

    public override void Attack(Entity target)
    {
        if (_audioPlayer != null && _isCanAttack)
        {
            _audioPlayer.Play("Attack", UnityEngine.Random.Range(0.9f, 1.1f));
        }

        base.Attack(target);
    }
}
