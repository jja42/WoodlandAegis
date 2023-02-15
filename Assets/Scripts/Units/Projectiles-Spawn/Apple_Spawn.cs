using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple_Spawn : MonoBehaviour
{
    public Apple parent;
    public int duration;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy_AI enemy = collision.gameObject.GetComponent<Enemy_AI>();
        if (!enemy.is_distracted)
        {
            enemy.Distract(duration);
            parent.spawn_count--;
            Destroy(gameObject);
        }
    }
}
