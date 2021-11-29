using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troops : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform secondPoint;
    private Transform wayPoint;

    // Start is called before the first frame update
    void Start()
    {
        wayPoint = startPoint;
        InvokeRepeating("Move", 1.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, startPoint.position) < 0.2f){
            wayPoint = secondPoint;
        }

        if(Vector2.Distance(transform.position, secondPoint.position) < 0.2f){
            wayPoint = startPoint;
        }

        if(GameManager.Instance.enemies.Count <= 0){
            Destroy(transform.parent.gameObject);
        }
    }

    void Move(){
        MoveToward(wayPoint);
    }

    protected void MoveToward(Transform selectedTarget)
    {
        transform.position = Vector2.MoveTowards(transform.position, selectedTarget.position, Time.deltaTime * moveSpeed);
    }

    
}
