using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime = 1;
    [SerializeField] private Vector2 dirShoot = Vector2.zero;

    // Start is called before the first frame update
    public void Shoot(Vector2 direction){
        dirShoot = direction;
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(dirShoot * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Enemy")){
            IDamagable damagable = other.GetComponent<IDamagable>();
            if(damagable != null){
                damagable.ApplyDamage(1);
            }
            DestroyProjectile();
        }

        if(other.CompareTag("Guard")){
            IDamagable damagable = other.GetComponent<IDamagable>();
            if(damagable != null){
                damagable.ApplyDamage(1);
            }
            DestroyProjectile();
        }

        if(other.CompareTag("ProjectileEnemy")){
            Debug.Log("Laser is Collided2");
            other.gameObject.SetActive(false);
            DestroyProjectile();
        }
    }

    protected void DestroyProjectile()
    {
        gameObject.SetActive(false);
        CancelInvoke();
    }
}
