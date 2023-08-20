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

            GetComponent<BaseEnemyStateMachine>().enabled = false;
            GetComponent<EnemyTargetSelector>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;

            EnemyDied?.Invoke(GetComponent<EnemyEntity>());

            GlobalEventManager.SendEnemyDied(GetMoneyForKilling());

            base.Die();
        }
    }

    public override void Attack(Entity target)
    {
        if (AudioPlayer != null && IsCanAttack)
        {
            AudioPlayer.Play("Attack", UnityEngine.Random.Range(0.9f, 1.1f));
        }

        base.Attack(target);
    }
}
