using UnityEngine;
using System.Collections;

public class GunController : Item
{
    [SerializeField]
    private Transform bulletPosition;
    [SerializeField]
    private GameObject bulletPrefab;

    ObjectPool<BulletController> bulletPool;
    int poolSize = 3;
    void Start()
    {
        bulletPool = new ObjectPool<BulletController>(poolSize, bulletPrefab);
    }

    public override void AnimationEvent()
    {
        BulletController bullet = bulletPool.Get();
        bullet.Create(bulletPosition);
    }

    private void LookAtPlayer()
    {

    }
}
