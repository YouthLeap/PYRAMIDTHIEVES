using UnityEngine;
using System.Collections;

public class BlowGunController : Trap
{
    [SerializeField]
    private Transform bulletPosition;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private int poolSize = 5;
    private ObjectPool<BulletController> bulletPool;

    void Start()
    {
        bulletPool = new ObjectPool<BulletController>(poolSize, bulletPrefab);
    }

    public void Fire()
    {
        BulletController bullet = bulletPool.Get();
        bullet.Create(bulletPosition);
    }


}
