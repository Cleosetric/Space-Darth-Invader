using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Enemy
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float agroRange;
    [SerializeField] private float distance;

    private Transform playerTransform;
    private bool isActive = false;

    protected override void Start()
    {
        base.Start();
        col2D.enabled = false;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        float rngNextTime = UnityEngine.Random.Range(3, 5);
        Invoke("ActiveHostile", 1.5f);
    }

    private void ActiveHostile(){
        col2D.enabled = true;
        isActive = true;
    }

    protected override void MoveSet()
    { 
        if(isActive) Chase();
    }

    private void Chase()
    {
        if(playerTransform == null) return;
        
        if (Vector3.Distance(transform.position, playerTransform.position) < agroRange) 
        {  
            transform.LookAt(playerTransform.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        }

        if (Vector3.Distance(transform.position, playerTransform.position) < agroRange) 
        {  
            if (Vector3.Distance(transform.position, playerTransform.position) > distance)
            {
                transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            IDamagable damagable = other.GetComponent<IDamagable>();
            if(damagable != null){
                damagable.ApplyDamage(1);
                Die();
            }
        }
    }
}
