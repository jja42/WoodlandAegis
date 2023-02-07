using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemon : MonoBehaviour
{
    Transform target;
    Enemy_AI enemy;
    public GameObject projectile;
    float shoot_timer = 1.25f;
    private void Update()
    {
        if (Game_Manager.instance.started && !Game_Manager.instance.paused)
        {
            if (shoot_timer >= 1.25f && target != null)
            {
                Shoot();
            }
            else
            {
                shoot_timer += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.transform;
        enemy = target.gameObject.GetComponent<Enemy_AI>();
    }

    void Shoot()
    {
        if(enemy.health <= 0)
        {
            target = null;
            return;
        }
        if (target != null)
        {
            GameObject obj = Instantiate(projectile, transform.position, Quaternion.identity);
            Projectile proj = obj.GetComponent<Projectile>();
            proj.target = target;
            shoot_timer = 0;
        }
    }
}
