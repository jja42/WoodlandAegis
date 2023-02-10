using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Enemy_AI
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        damage = 10;
        health = 80;
        move_speed = 5;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
