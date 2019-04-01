using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySimple : MonoBehaviour
{
    public int health;
    NavMeshAgent npc_agent;
    float dummy_timer;
    bool path_select;

    private void Start()
    {
        npc_agent = GetComponent<NavMeshAgent>();
        path_select = true;
    }

    void Update()
    {
      /*  dummy_timer += Time.deltaTime;

        if(dummy_timer >= 3.0f)
        {
            Vector3 new_dest;
            if(path_select)
            {
                new_dest = new Vector3(-8, 0, 18);
            }
            else
            {
                new_dest = new Vector3(2, 0, 18);
            }
            npc_agent.SetDestination(new_dest);
            dummy_timer = 0.0f;
            path_select = !path_select;
        }*/
    
    }

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
