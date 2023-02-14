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
            Instantiate(projectiles, transform.position, Quaternion.identity, transform);
            act_timer = 0;
        }
    }
}
