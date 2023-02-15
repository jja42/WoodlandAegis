using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : Unit
{
    List<Map_Manager.Node> path_nodes;
    public GameObject spawn;
    public int spawn_max;
    public int spawn_count;
    int spawn_index;
    float spawn_x_offset;
    float spawn_y_offset;
    protected override void Start()
    {
        act_timeframe = 2.5f;
        path_nodes = Map_Manager.instance.GetPathNodes();
    }

    protected override void Act()
    {
        Spawn();
    }

    void Spawn()
    {
        if (spawn_count < spawn_max)
        {
            spawn_index = Random.Range(0, path_nodes.Count);
            spawn_x_offset = Random.Range(-.25f, .25f);
            spawn_y_offset = Random.Range(-.25f, .25f);
            GameObject obj = Instantiate(spawn, path_nodes[spawn_index].pos + new Vector3(spawn_x_offset, spawn_y_offset), Quaternion.identity);
            Corn_Spawn corn = obj.GetComponent<Corn_Spawn>();
            corn.parent = this;
            spawn_count++;
        }
        act_timer = 0;
    }
}
