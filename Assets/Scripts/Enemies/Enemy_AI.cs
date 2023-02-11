using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    List<Vector3> path;
    Vector3 end;
    bool pathfinding;
    int current_node;
    public float move_speed;
    public Animator anim;
    Direction direction;
    Direction pastDirection;
    protected int damage;
    public int health;
    public int id;
    protected bool fainting;
    protected Collider2D col;
    protected bool fast;
    public enum Direction
    {
        Down,
        Up,
        Left,
        Right,
        None
    }

    protected virtual void Start()
    {
        current_node = 0;
        direction = Direction.None;
        pastDirection = direction;
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        path = Map_Manager.instance.path;
        pathfinding = true;
        end = path[path.Count - 1] + (path[path.Count - 1] - path[path.Count - 2]);
        path.Add(end);
        ChangeDir();
    }
    protected virtual void Update()
    {
        if (!Game_Manager.instance.paused && Game_Manager.instance.started)
        {
            if (pathfinding)
            {
                Pathfind();
            }
        }
    }

    void ChangeDir()
    {
        if (current_node >= path.Count)
        {
            return;
        }
        Vector3 dir = path[current_node] - transform.position;
        dir = dir.normalized;
        if (dir.x > 0)
        {
            direction = Direction.Right;
        }
        if (dir.x < 0)
        {
            direction = Direction.Left;
        }
        if (dir.y > 0)
        {
            direction = Direction.Up;
        }
        if (dir.y < 0)
        {
            direction = Direction.Down;
        }

        switch (direction)
        {
            case Direction.Down:
                if (pastDirection != direction)
                {
                    anim.SetTrigger("MoveDownT");
                    pastDirection = direction;
                }

                break;

            case Direction.Up:
                if (pastDirection != direction)
                {
                    anim.SetTrigger("MoveUpT");
                    pastDirection = direction;
                }

                break;

            case Direction.Left:
                if (pastDirection != direction)
                {
                    anim.SetTrigger("MoveLeftT");
                    pastDirection = direction;
                }
                break;

            case Direction.Right:
                if (pastDirection != direction)
                {
                    anim.SetTrigger("MoveRightT");
                    pastDirection = direction;
                }
                break;

            default:
                break;
        }
        //print("Direction: "+ dir + " " + direction);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            Faint();
        }
    }

    void Faint()
    {
        if (!fainting)
        {
            col.enabled = false;
            fainting = true;
            pathfinding = false;
            anim.SetTrigger("FaintT");
            Game_Manager.instance.RemoveEnemy(id);
            Destroy(gameObject, 1.5f);
        }
    }

    void Pathfind()
    {
        if (transform.position == path[current_node])
        {
            current_node += 1;
            ChangeDir();
        }
        if (current_node < path.Count)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[current_node], Time.deltaTime * move_speed);
            return;
        }
        else
        {
            pathfinding = false;
            Game_Manager.instance.TakeDamage(damage);
            Game_Manager.instance.RemoveEnemy(id);
            Destroy(gameObject, 1);
            return;
        }
    }
}
