using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn_Spawn : MonoBehaviour
{
    public Corn parent;
    SpriteRenderer sr;
    public Sprite explosion;
    bool exploded;
    public int damage;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!exploded)
        {
            sr.sprite = explosion;
            exploded = true;
            Enemy_AI enemy = collision.gameObject.GetComponent<Enemy_AI>();
            enemy.TakeDamage(damage);
            parent.spawn_count--;
            Destroy(gameObject, 1);
        }
    }
}
