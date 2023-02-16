using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Transform target;
    public Enemy_AI enemy;
    protected float act_timer;
    protected float act_timeframe;
    public int sell_value;
    public float range;
    public GameObject range_indicator;

    private void OnMouseEnter()
    {
        if(range != 0)
        {
            range_indicator.SetActive(true);
        }
    }
    private void OnMouseExit()
    {
        if (range_indicator != null)
        {
            range_indicator.SetActive(false);
        }
    }
    protected virtual void Start()
    {
        act_timeframe = 1.25f;
    }
    protected virtual void Update()
    {
        if (Game_Manager.instance.started && !Game_Manager.instance.paused)
        {
            if (act_timer >= act_timeframe)
            {
                Act();
            }
            else
            {
                act_timer += Time.deltaTime;
            }
        }
    }

    protected virtual void Act()
    {

    }
}
