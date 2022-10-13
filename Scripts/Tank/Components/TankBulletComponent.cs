using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;

public class TankBulletComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeedInNewtons;

    public void SpawnBullet()
    {
        var bullet = Instantiate(this.bulletPrefab, this.gameObject.transform);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), this.gameObject.GetComponent<Collider>(), true);
        bullet.GetComponent<Rigidbody>().AddForce(this.bulletSpeedInNewtons * this.transform.forward, ForceMode.VelocityChange);
        bullet.transform.SetParent(null);
    }
}
