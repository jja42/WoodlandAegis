using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : Enemy_AI
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        damage = 10;
        health = 20;
        move_speed = 2.5f;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}