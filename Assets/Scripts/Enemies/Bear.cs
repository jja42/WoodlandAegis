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
    }
}
