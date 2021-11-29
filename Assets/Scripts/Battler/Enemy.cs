using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public delegate void OnEnemyDied(Enemy enemy);
    public OnEnemyDied onEnemyDied;

    [SerializeField] private int maxLife = 1;
    [SerializeField] private int score = 10;

    public int life { get ; set; }

    protected BoxCollider2D col2D;


    protected virtual void Start()
    {
        col2D = GetComponent<BoxCollider2D>();
        life = maxLife;
    }

    public void ApplyDamage(int damage)
    {
        life -= damage;
        if(life <= 0) life = 0;
    }

    private void Update()
    {
        MoveSet();
        CheckLife();
    }

    protected virtual void MoveSet()
    {
       //TODO: Nothing todo here
    }

    protected void CheckLife()
    {
        if(life <= 0){
            Die();
        }
    }

    protected virtual void Die(){
        GameManager.Instance.AddScore(score);
        if(onEnemyDied != null) onEnemyDied.Invoke(this);
        InstantiateCollectible();
        Destroy(gameObject);
    }

    private void InstantiateCollectible()
    {
        float buffSpawnChance = UnityEngine.Random.Range(0, 100f);
        if (buffSpawnChance <= GameManager.Instance.BuffChance)
        {
            Collectibles buff = GameManager.Instance.buff;
            Collectibles newCollectable = Instantiate(buff, this.transform.position, Quaternion.identity) as Collectibles;
        }
    }
}
