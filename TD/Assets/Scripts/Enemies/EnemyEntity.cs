using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyEntity : Entity
{
    public Action<EnemyEntity> EnemyDied;

    protected override void Die()
    {
        if (!IsSignalSended)
        {
            IsSignalSended = true;

            EnemyDied?.Invoke(GetComponent<EnemyEntity>());

            GlobalEventManager.SendEnemyDied(GetMoneyForKilling());

            base.Die();
        }
    }

    public override void Attack(Entity target)
    {
        if (AudioPlayer != null && IsCanAttack)
        {
            AudioPlayer.Play(SoundTypes.Attack, UnityEngine.Random.Range(0.9f, 1.1f));
        }

        base.Attack(target);
    }
}
