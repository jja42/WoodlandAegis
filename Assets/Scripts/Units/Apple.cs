using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Unit
{
    List<Map_Manager.Node> nodes_in_range;
    public GameObject spawn;
    public int spawn_max;
    public int spawn_count;
    int spawn_index;
    float spawn_x_offset;
    float spawn_y_offset;
    protected override void Start()
    {
        act_timeframe = 4f;
        nodes_in_range = Map_Manager.instance.GetUnitPathNeighbors(transform.position);
    }

    protected override void Act()
    {
        Spawn();
    }

    void Spawn()
    {
        if (spawn_count < spawn_max)
        {
            spawn_index = Random.Range(0, nodes_in_range.Count);
            spawn_x_offset = Random.Range(-.25f, .25f);
            spawn_y_offset = Random.Range(-.25f, .25f);
            GameObject obj = Instantiate(spawn, nodes_in_range[spawn_index].pos + new Vector3(spawn_x_offset, spawn_y_offset), Quaternion.identity);
            Apple_Spawn apple = obj.GetComponent<Apple_Spawn>();
            apple.parent = this;
            spawn_count++;
        }
        act_timer = 0;
    }
}
