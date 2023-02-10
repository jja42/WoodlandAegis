using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : Enemy_AI
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        damage = 50;
        health = 150;
        move_speed = 2.5f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
