using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public List<int> spawn_amounts;
    public List<float> spawn_delays;
    int index;
    public float spawn_delay;
    float timer;
    public int spawn_amount;
    public GameObject enemy;
    public Vector3 spawnpos;
    bool spawning;
    bool stop;
    int id;

    private void Start()
    {
        index = 0;
        GetSpawnVals(index);
        spawning = true;
        timer = spawn_delay;
    }
    private void Update()
    {
        if (Game_Manager.instance.started && !Game_Manager.instance.paused)
        {
            if (timer >= spawn_delay && spawning)
            {
                Spawn();
            }
            else
            {
                timer += Time.deltaTime;
                if (Game_Manager.instance.enemies.Count == 0 && !stop && spawn_amount <= 0)
                {
                    UpdateRound();
                }
            }
        }
    }
    void UpdateRound()
    {
        Game_Manager.instance.ProgressRound();
        if (Game_Manager.instance.current_round <= Game_Manager.instance.last_round)
        {
            index = Game_Manager.instance.current_round - 1;
            GetSpawnVals(index);
        }
        else
        {
            stop = true;
        }
    }

    void GetSpawnVals(int index)
    {
        spawn_amount = spawn_amounts[index];
        spawn_delay = spawn_delays[index];
        spawning = true;
        timer = 0;
    }

    void Spawn()
    {
        timer = 0;
        if (spawn_amount > 0)
        {
            GameObject obj = Instantiate(enemy, spawnpos, Quaternion.identity);
            Enemy_AI ai = obj.GetComponent<Enemy_AI>();
            ai.id = id;
            id++;
            spawn_amount--;
            Game_Manager.instance.enemies.Add(ai);
        }
    }
}
