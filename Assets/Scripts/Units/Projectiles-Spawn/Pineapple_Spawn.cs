using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pineapple_Spawn : MonoBehaviour
{
    public Pineapple parent;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy_AI enemy = collision.gameObject.GetComponent<Enemy_AI>();
        enemy.TakeDamage(damage);
        parent.spawn_count--;
        Destroy(gameObject);
    }
}
