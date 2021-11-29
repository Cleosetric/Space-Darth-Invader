using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGuard : MonoBehaviour, IDamagable
{
    [SerializeField] private int maxLife = 10;
    public int life { get ; set; }

    private void Start()
    {
        life = maxLife;
    }

    public void ApplyDamage(int damage)
    {
        life -= damage;
        if(life <= 0) life = 0;
    }

    private void Update() {
        CheckLife();
    }

    private void CheckLife()
    {
        if(life <= 0){
            Destroy(gameObject);
        }
    }
}
