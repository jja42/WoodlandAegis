using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sell_Object : MonoBehaviour
{
    Vector3 last_position;
    public SpriteRenderer indicator;
    public bool valid_pos;
    public Color red;
    public Color green;
    public bool Unit;
    private void Update()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector3.Distance(transform.position, last_position) >= .25f)
        {
            last_position = transform.position;
            valid_pos = ValidatePosition();
            UpdateIndicator();
        }
    }

    bool ValidatePosition()
    {
        if (Map_Manager.instance.IsPath(transform.position))
        {
            return false;
        }
        Unit = Map_Manager.instance.IsUnit(transform.position);
        return Map_Manager.instance.CheckNode(transform.position, !Unit, true);
    }

    void UpdateIndicator()
    {
        if (valid_pos)
        {
            indicator.color = green;
        }
        else
        {
            indicator.color = red;
        }
    }

    private void OnEnable()
    {
        last_position = transform.position;
        ValidatePosition();
    }
}
