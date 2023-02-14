using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy_AI
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        damage = 20;
    }
}
