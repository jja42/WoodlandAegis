using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetting : MonoBehaviour
{
    Unit parent;
    private void Start()
    {
        parent = GetComponentInParent<Unit>();
    }
    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (parent.target == null)
        {
            parent.target = collision.transform;
            parent.enemy = parent.target.gameObject.GetComponent<Enemy_AI>();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        parent.target = null;
        parent.enemy = null;
    }
}
