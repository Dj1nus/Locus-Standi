public class MinerEntity : Entity
{
    private bool _isOnDestroySignalSended = false;

    protected override void Die()
    {
        if (!_isOnDestroySignalSended)
        {
            _isOnDestroySignalSended = true;
            GlobalEventManager.SendBuildingDestroy(GetComponent<MapUnit>());
        }

        base.Die();
    }
}
