using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public int speed;
    public float range;
    Vector3 startpos;
    public Vector3 direction;

    private void Start()
    {
        startpos = transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy_AI enemy = collision.gameObject.GetComponent<Enemy_AI>();
        enemy.TakeDamage(damage);
        Destroy(gameObject);
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, startpos) > range)
        {
            Destroy(gameObject);
            return;
        }
        if (!Game_Manager.instance.paused)
            transform.position += (speed * Time.deltaTime * direction);
    }
}
