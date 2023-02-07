using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;
    public int current_round;
    public int health;
    public int nutrients;
    public int nutrient_rate;
    public bool paused;
    bool can_pause;
    public bool started;
    public int last_round;
    float update_timer;
    public List<Enemy_AI> enemies;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        current_round = 1;
        health = 100;
        nutrients = 100;
        nutrient_rate = 5;
        last_round = 5;
        can_pause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused && started)
        {
            update_timer += Time.deltaTime;
            if(update_timer >= 1)
            {
                UpdateNutrients(nutrient_rate);
                update_timer = 0;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            paused = true;
            can_pause = false;
            Loss();
        }
        UI_Manager.instance.UpdateHealthText(health);
    }
    public void ProgressRound()
    {
        current_round++;
        if(current_round > last_round)
        {
            paused = true;
            can_pause = false;
            Victory();
        }
        else
        {
            UI_Manager.instance.UpdateRoundText(current_round);
        }
    }

    public void UpdateNutrients(int val)
    {
        nutrients += val;
        UI_Manager.instance.UpdateNutrientText(nutrients);
    }

    public void UpdateNutrientRate(int rate)
    {
        nutrient_rate += rate;
        UI_Manager.instance.UpdateNutrientRateText(nutrient_rate);
    }

    void Victory()
    {
        UI_Manager.instance.Victory_UI();
    }

    void Loss()
    {
        UI_Manager.instance.Loss_UI();
    }

    public void Pause_Unpause()
    {
        if (can_pause)
        {
            paused = !paused;
            UI_Manager.instance.Pause();
        }
    }

    public void Begin()
    {
        started = true;
        UI_Manager.instance.Begin();
    }

    public void RemoveEnemy(int id)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].id == id)
            {
                enemies.Remove(enemies[i]);
                break;
            }
        }
    }
}
