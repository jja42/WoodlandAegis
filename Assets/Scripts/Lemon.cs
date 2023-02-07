using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemon : MonoBehaviour
{
    Transform target;
    public GameObject projectile;
    float shoot_timer = 1.25f;
    private void Update()
    {
        if (Game_Manager.instance.started && !Game_Manager.instance.paused)
        {
            if (shoot_timer >= 1.25f)
            {
                Shoot();
                shoot_timer = 0;
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
    }

    void Shoot()
    {
        if (target != null)
        {
            GameObject obj = Instantiate(projectile, transform.position, Quaternion.identity);
            Projectile proj = obj.GetComponent<Projectile>();
            proj.target = target;
        }
    }
}
