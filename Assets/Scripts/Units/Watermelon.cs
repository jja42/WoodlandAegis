using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon : Unit
{
    public GameObject projectiles;
    public float range;
    protected override void Act()
    {
        Shoot();
    }
    void Shoot()
    {
        if (enemy != null)
        {
            if (enemy.health <= 0)
            {
                target = null;
                return;
            }
        }
        if (target != null)
        {
            GameObject obj = Instantiate(projectiles, transform.position, Quaternion.identity, transform);
            Projectile[] projectile_array = obj.GetComponentsInChildren<Projectile>();
            foreach (Projectile proj in projectile_array)
            {
                proj.range = range;
            }
            act_timer = 0;
        }
    }
}
