using UnityEngine;

public class RocketTurretTargetSelector : TurretTargetSelector
{
    public override void ChooseTarget()
    {
        if (_targets.Count <= 0) return;

        int maxOverlapedBodies = 0;
        int overlapedBodies = 0;
        EnemyEntity possibleTarget = null;

        foreach (EnemyEntity target in _targets)
        {
            if (target == null || target.GetHp() <= 0) continue;

            overlapedBodies = Physics.OverlapSphere(target.transform.position, 2.5f).Length;

            if (overlapedBodies > maxOverlapedBodies)
            {
                possibleTarget = target;
                maxOverlapedBodies = overlapedBodies;
            }
        }

        if (possibleTarget != null)
        {
            SetTarget(possibleTarget);
            RemoveTarget(possibleTarget);
            return;
        }
    }
}
