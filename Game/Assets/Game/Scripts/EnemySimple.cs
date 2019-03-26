using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimple : MonoBehaviour
{
    public int health;  

    public void Hurt(int value)
    {
        if (value > health)
        {
            health = 0;
        }
        else
        {
            health -= value;
        }

        if (health <= 0)
            Destroy(gameObject);
    }
}
