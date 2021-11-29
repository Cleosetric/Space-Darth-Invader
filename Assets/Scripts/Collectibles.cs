using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectibles : MonoBehaviour
{
    private void Start() {
        Invoke("DestroyCollectible", 5f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            Ship ship = other.GetComponent<Ship>();
            if(ship != null){
                ApplyEffect(ship);
            }
        }   
    }

    protected virtual void ApplyEffect(Ship ship){
        ship.SwapWeaponType();
        DestroyCollectible();
    }

    private void DestroyCollectible(){
        Destroy(gameObject);
        CancelInvoke();
    }
}
