using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lemon : Unit
{
    public GameObject projectile;
    public float range;

    protected override void Act()
    {
        Shoot();
    }
    void Shoot()
    {
        if (enemy.health <= 0)
        {
            target = null;
            return;
        }
        if (target != null)
        {
            GameObject obj = Instantiate(projectile, transform.position, Quaternion.identity, transform);
            Projectile proj = obj.GetComponent<Projectile>();
            proj.range = range;
            proj.target = target;
            act_timer = 0;
        }
    }
}
