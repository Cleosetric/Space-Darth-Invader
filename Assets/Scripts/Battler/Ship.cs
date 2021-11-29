using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponType {single, triple}

public class Ship : MonoBehaviour, IDamagable
{
    [SerializeField] private int maxLife = 3;
    [SerializeField] private Image hpBar;
    [SerializeField] private Image hpEffect;

    public int life { get; set; }
    
    [Space]
    public WeaponType weaponType = WeaponType.single;

    private Coroutine shootType;
    private Coroutine blink;


    // Start is called before the first frame update
    private void Start()
    {
        life = maxLife;
    }

    public void ApplyDamage(int damage){
        life -= damage;
        if(life <= 0) life = 0;

        if(blink != null) StopCoroutine(blink);
        blink = StartCoroutine(Blinks(3,0.15f));
    }

    public void Attack(){       
        switch (weaponType)
        {
            case WeaponType.single:
                SingleShoot();
            break;
            case WeaponType.triple:
                TripleShoot();
            break;
            default:
                SingleShoot();
            break;
        }
    }
    
    public void SwapWeaponType(){
        if(shootType != null) StopCoroutine(shootType);
        shootType = StartCoroutine(SwapWeapon());
    }

    private void Update() {
        CheckLife();
        DisplayHPBar();
    }

    private void DisplayHPBar()
    {
        hpBar.fillAmount = (float)life / (float)maxLife;
        if(hpEffect.fillAmount > hpBar.fillAmount){
            hpEffect.fillAmount -= 0.005f;
        }else{
            hpEffect.fillAmount = hpBar.fillAmount;
        }
    }

    private void CheckLife()
    {
        if(life <= 0){
            //TODO : Instantiate(explosion, transform.position, Quaternion.identity);
            //TODO : animator.SetBool("DeadAnim", true);
            //TODO : bool isAlive = false;
            //TODO : gameObject.SetActive(false);
            //TODO : GameManager.Instance.Dead()
            GameManager.Instance.GameOver();
            gameObject.SetActive(false);
        }
    }

    private void TripleShoot(){
        Vector3 direction = Vector2.up;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float offset = -75f;
        for (int i = 0; i < 3; i++)
        {
            GameObject laser = ObjectPooling.Instance.GetPooledObject("Projectile");
            if (laser != null) {
                laser.transform.position = transform.position + Vector3.up;
                laser.transform.localRotation = Quaternion.Euler(0, 0, angle + offset);
                laser.GetComponent<Projectile>().Shoot(Vector2.up);
                laser.SetActive(true);
                offset -= 15f;
            }
        }

        // Instantiate(projectile.gameObject, transform.position, Quaternion.Euler(0, 0, angle + -75f));
        // Instantiate(projectile.gameObject, transform.position, Quaternion.Euler(0, 0, angle + -90f));
        // Instantiate(projectile.gameObject, transform.position, Quaternion.Euler(0, 0, angle + -105f));
    }

    private void SingleShoot(){
        GameObject laser = ObjectPooling.Instance.GetPooledObject("Projectile");
        if(laser != null){
            laser.transform.position = transform.position + Vector3.up;
            laser.transform.rotation = Quaternion.identity;
            laser.GetComponent<Projectile>().Shoot(Vector2.up);
            laser.SetActive(true);
        }
        // Instantiate(projectile.gameObject, transform.position, Quaternion.identity);
    }

    public IEnumerator Blinks(int numBlinks, float seconds)
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        collider.enabled = false;
        for (int i = 0; i < numBlinks * 2; i++)
        {
            renderer.enabled = !renderer.enabled;
            yield return new WaitForSeconds(seconds);
        }
        renderer.enabled = true;
        collider.enabled = true;
        blink = null;
    }

    private IEnumerator SwapWeapon(){
        weaponType = WeaponType.triple;
        yield return new WaitForSeconds(5);
        weaponType = WeaponType.single;
        shootType = null;
    }
}
