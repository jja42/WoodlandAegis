using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potato_Spawn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy_AI enemy = collision.gameObject.GetComponent<Enemy_AI>();
        enemy.Poison(10, 5);
    }
}
