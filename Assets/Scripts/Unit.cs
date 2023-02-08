using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected Transform target;
    protected Enemy_AI enemy;
    protected float act_timer;
    protected float act_timeframe;
    public int sell_value;

    protected virtual void Start()
    {
        act_timeframe = 1.25f;
    }
    protected virtual void Update()
    {
        if (Game_Manager.instance.started && !Game_Manager.instance.paused)
        {
            if (act_timer >= act_timeframe && target != null)
            {
                Act();
            }
            else
            {
                act_timer += Time.deltaTime;
            }
        }
    }

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (target == null)
        {
            target = collision.transform;
            enemy = target.gameObject.GetComponent<Enemy_AI>();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
        enemy = null;
    }

    protected virtual void Act()
    {

    }
}
