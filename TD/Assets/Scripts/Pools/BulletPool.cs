using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private int _poolCount = 50;
    [SerializeField] private bool _isAutoExpand = true;
    [SerializeField] private Bullet _bulletPrefab;

    private PoolMono<Bullet> _bulletPool;

    private void Start()
    {
        _bulletPool = new PoolMono<Bullet>(_bulletPrefab, _poolCount, transform);
        _bulletPool.IsAutoExpand = _isAutoExpand;
    }

    public Bullet CreateBullet()
    {
        return _bulletPool.GetFreeElement();
    }
}
