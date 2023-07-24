using System;

public class TurretEntity : Entity
{
    private bool _isOnDestroySignalSended = false;
    public Action<TurretEntity> OnDestroy;


    protected override void Die()
    {
        
        if (!_isOnDestroySignalSended)
        {
            _isOnDestroySignalSended = true;

            OnDestroy?.Invoke(GetComponent<TurretEntity>());
            GlobalEventManager.SendBuildingDestroy(GetComponent<MapUnit>());
        }

        Destroy(gameObject);
        //base.Die();
    }
}
