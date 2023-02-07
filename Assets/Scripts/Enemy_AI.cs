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
    int damage;
    int health;
    public int id;
    bool fainting;
    public enum Direction
    {
        Down,
        Up,
        Left,
        Right,
        None
    }

    private void Start()
    {
        current_node = 0;
        direction = Direction.None;
        pastDirection = direction;
        anim = GetComponent<Animator>();
        damage = 10;
        health = 30;
    }
    private void Update()
    {
        if (!Game_Manager.instance.paused && Game_Manager.instance.started)
        {
            if (path == null)
            {
                path = Map_Manager.instance.path;
                pathfinding = true;
                end = path[path.Count-1] + (path[path.Count - 1] - path[path.Count - 2]);
                path.Add(end);
                ChangeDir();
            }
            if (pathfinding)
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
                    Destroy(gameObject,1);
                    return;
                }
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
        if (dir.x > 0 && Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            direction = Direction.Right;
        }
        if (dir.x < 0 && Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            direction = Direction.Left;
        }
        if (dir.y > 0 && Mathf.Abs(dir.y) > Mathf.Abs(dir.x))
        {
            direction = Direction.Up;
        }
        if (dir.y < 0 && Mathf.Abs(dir.y) > Mathf.Abs(dir.x))
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
            fainting = true;
            pathfinding = false;
            anim.SetTrigger("FaintT");
            Game_Manager.instance.RemoveEnemy(id);
            Destroy(gameObject, 1.5f);
        }
    }
}
