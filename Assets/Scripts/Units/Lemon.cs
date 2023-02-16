using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemon : Unit
{
    public GameObject projectile;
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
            GameObject obj = Instantiate(projectile, transform.position, Quaternion.identity, transform);
            Seeking_Projectile proj = obj.GetComponent<Seeking_Projectile>();
            proj.target = target;
            act_timer = 0;
        }
    }
}
