using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform target;
    public int damage;
    public int speed;

    private void Start()
    {
        Destroy(gameObject, 2);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (target == null)
            Destroy(gameObject);
        if(collision.transform == target)
        {
            Enemy_AI enemy = target.gameObject.GetComponent<Enemy_AI>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }
        if(!Game_Manager.instance.paused)
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
    }
}
