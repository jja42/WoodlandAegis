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
    public float range;
    public LayerMask enemy_Mask;
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
            Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, range,enemy_Mask);
            List<Enemy_AI> enemies = new List<Enemy_AI>();
            foreach (Collider2D col in cols)
            {
                enemies.Add(col.GetComponent<Enemy_AI>());
            }
            foreach (Enemy_AI enemy in enemies)
            {
                enemy.TakeDamage(damage);
            }
            parent.spawn_count--;
            Destroy(gameObject, 1);
        }
    }
}
