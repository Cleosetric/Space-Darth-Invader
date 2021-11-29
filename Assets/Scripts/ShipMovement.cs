using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] private float boundMinX;
    [SerializeField] private float boundMaxX;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dampeningSpeed;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireTime;

    private Ship ship;
    private Coroutine moveDamp;
    private float nextFireTime = 0;

    // Start is called before the first frame update
    private void Start()
    {
        ship = GetComponent<Ship>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateMovement();
        UpdateShoot();
    }

    private void UpdateShoot()
    {
        if(Input.GetKey(KeyCode.Space)){
            if(Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + fireTime / fireRate;
            }
        }
    }

    void UpdateMovement(){
        if(Input.GetKey(KeyCode.LeftArrow)){
            if(moveDamp != null) StopCoroutine(moveDamp);
            if(transform.position.x >= boundMinX){
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime); 
            } 
        }else if(Input.GetKeyUp(KeyCode.LeftArrow)){
            if(transform.position.x >= boundMinX + 1){
                if(moveDamp != null) StopCoroutine(moveDamp);
                moveDamp = StartCoroutine(moveDampening(Vector3.left));
            }
        } 
        
        if(Input.GetKey(KeyCode.RightArrow)){
            if(moveDamp != null) StopCoroutine(moveDamp);
            if(transform.position.x <= boundMaxX){
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime); 
            }
        }else if(Input.GetKeyUp(KeyCode.RightArrow)){
            if(transform.position.x <= boundMaxX - 1){
                if(moveDamp != null) StopCoroutine(moveDamp);
                moveDamp = StartCoroutine(moveDampening(Vector3.right));
            }
        } 
    }

    void Shoot(){
        Debug.Log("Shoot Laser!");
        ship.Attack();
    }

    IEnumerator moveDampening(Vector3 dir){
        Vector3 destination = transform.position + dir;
        float timeRemaining = 0;
        while (timeRemaining <= 1f)
        {
            transform.position = Vector3.Lerp(transform.position, destination, dampeningSpeed * Time.deltaTime);
            timeRemaining += Time.deltaTime;
            yield return null;
        }
        transform.position = destination;
        moveDamp = null;
    }
}
