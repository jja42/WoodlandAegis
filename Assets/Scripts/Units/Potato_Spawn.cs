using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato_Spawn : MonoBehaviour
{
    public Potato parent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy_AI enemy = collision.gameObject.GetComponent<Enemy_AI>();
        if (!enemy.is_poisoned)
        {
            enemy.Poison(10, 5);
            parent.spawn_count--;
            Destroy(gameObject);
        }
    }
}
