using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyEntity : Entity
{
    public Action<EnemyEntity> EnemyDied;

    protected override void Die()
    {
        if (!_isSignalSended)
        {
            _isSignalSended = true;
            EnemyDied?.Invoke(GetComponent<EnemyEntity>());
            GlobalEventManager.SendEnemyDied(GetMoneyForKilling());

            GetComponent<BaseEnemyStateMachine>().enabled = false;
            GetComponent<EnemyTargetSelector>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;

            base.Die();
        }
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
