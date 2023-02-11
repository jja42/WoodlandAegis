using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawner : MonoBehaviour
{
    public List<string> round_spawns;
    public List<float> spawn_delays;
    int round_index;
    
    public List<string> spawn_list;
    public float spawn_delay;
    float timer;
    public int spawn_amount;
    public int total_spawn_amount;
    int spawn_index;

    public List<GameObject> enemies_to_spawn;
    public Vector3 spawnpos;
    bool spawning;
    bool stop;
    int id;
    public GameObject Enemies;

    private void Start()
    {
        round_index = 0;
        GetSpawnVals(round_index);
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
                if (Game_Manager.instance.enemies.Count == 0 && !stop && total_spawn_amount <= 0)
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
            round_index = Game_Manager.instance.current_round - 1;
            GetSpawnVals(round_index);
        }
        else
        {
            stop = true;
        }
    }

    void GetSpawnVals(int index)
    {   
        string[] strings = round_spawns[index].Split(",");
        spawn_list = strings.ToList();
        total_spawn_amount = spawn_list.Count;
        ParseSpawns();
        spawn_delay = spawn_delays[index/5];
        spawning = true;
        timer = 0;
    }

    void Spawn()
    {
        if (total_spawn_amount > 0)
        {
            timer = 0;
            GameObject obj = Instantiate(enemies_to_spawn[spawn_index], spawnpos, Quaternion.identity, Enemies.transform);
            Enemy_AI ai = obj.GetComponent<Enemy_AI>();
            ai.id = id;
            id++;
            spawn_amount--;
            Game_Manager.instance.enemies.Add(ai);
            if (spawn_amount == 0)
            {
                total_spawn_amount--;
                spawn_list.RemoveAt(0);
                if(total_spawn_amount > 0)
                    ParseSpawns();
            }
        }
        else
        {
            spawning = false;
        }
    }

    void ParseSpawns()
    {
        string[] substrings = spawn_list[0].Split("-");
        spawn_amount = int.Parse(substrings[1]);
        spawn_index = Spawn_Switch(substrings[0]);
    }

    int Spawn_Switch(string str)
    {
        switch (str)
        {
            case "B":
                return 0;
            case "FB":
                return 1;
            case "R":
                return 2;
            case "FR":
                return 3;
            case "W":
                return 4;
            case "FW":
                return 5;
            case "S":
                return 6;
            case "FS":
                return 7;
            default:
                return 0;
        }
    }
}
