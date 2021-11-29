using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy
{
    [SerializeField][Range(3,6)] private float maxFireTime = 6;
    [SerializeField][Range(3,6)] private float minFireTime = 3;
    [SerializeField] private float fireRate = 1;
    private float nextFireTime = 0;
    private bool isActive = false;

    protected override void Start()
    {
        base.Start();
        col2D.enabled = false;
        float rngNextTime = UnityEngine.Random.Range(minFireTime, maxFireTime);
        nextFireTime = rngNextTime;
        Invoke("ActiveHostile", 1.5f);
    }

    protected override void MoveSet()
    {
        base.MoveSet();
        Shoot();
    }

    private void ActiveHostile(){
        col2D.enabled = true;
        isActive = true;
    }

    private void Shoot(){
        if(isActive){
            float rngTime = UnityEngine.Random.Range(minFireTime, maxFireTime);
            if(Time.time >= nextFireTime)
            {
                GameObject laser = ObjectPooling.Instance.GetPooledObject("ProjectileEnemy");
                if(laser != null){
                    laser.transform.position = transform.position + Vector3.down;
                    laser.transform.rotation = Quaternion.identity;
                    laser.GetComponent<Projectile>().Shoot(Vector2.down);
                    laser.SetActive(true);
                }
                nextFireTime = Time.time + rngTime / fireRate;
            }
        }
    }
}
