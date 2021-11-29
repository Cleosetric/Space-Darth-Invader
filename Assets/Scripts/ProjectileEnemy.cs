using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : Projectile
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Guard")){
            IDamagable damagable = other.GetComponent<IDamagable>();
            if(damagable != null){
                damagable.ApplyDamage(1);
            }
            DestroyProjectile();
        }

        if(other.CompareTag("Projectile")){
            Debug.Log("Laser is Collided1");
            other.gameObject.SetActive(false);
            DestroyProjectile();
        }
    }
}
