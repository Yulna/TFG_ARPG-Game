using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySimple : MonoBehaviour
{
    public int health;
    public float base_attack_range;
    public float base_attack_speed;
    public float view_range;
    public float memory_time;
    public float attack_timer;
    NavMeshAgent npc_agent;
    Transform pc_transform;

    public int drop_chance;

    [SerializeField]
    float memory_timer;
    [SerializeField]
    bool player_found;

    private bool alive = true;

    private void Start()
    {
        alive = true;
        npc_agent = GetComponent<NavMeshAgent>();
        player_found = false;
        pc_transform = CharacterController.instance.move_controller.transform;          
    }

    void Update()
    {
        memory_timer += Time.deltaTime;
        attack_timer -= Time.deltaTime;

        if (!player_found)
        {
            Vector3 pc_distance = pc_transform.position - transform.position;
            if(pc_distance.magnitude <= view_range)
            {
                player_found = true;
                memory_timer = 0;
            }
            else if (memory_timer >= memory_time)
            {
                Debug.Log("Wandering...");
                npc_agent.SetDestination(Random.insideUnitSphere * 3);
                memory_timer = 0;
            }
        }
        else
        {           
            if (memory_timer >= memory_time)
            {
                player_found = false;
                npc_agent.SetDestination(transform.position);
            }
            else
                npc_agent.SetDestination(pc_transform.position);

            if(PathCompleted())
            {

                if (Vector3.Distance(pc_transform.position,transform.position) <= base_attack_range)
                {
                    attack_timer = 1 / base_attack_speed;
                    Collider[] hit_colliders = Physics.OverlapSphere(transform.position, base_attack_range);

                    for(int i = 0; i < hit_colliders.Length; i++)
                    {
                        if (hit_colliders[i].gameObject.tag == "Player") ;

                        Vector3 enemy_dir = hit_colliders[i].gameObject.transform.position - transform.position;
                        float player_angle = Vector3.Angle(enemy_dir, transform.position);
                        if (player_angle < 45f)
                        {
                            Debug.Log("player destructit");
                            CharacterController.instance.DamagePlayer(10, DamageType.DmgFire);
                        }
                    }                    
                }
            }

        }
    }

    private bool PathCompleted()
    {
        if (!npc_agent.pathPending && (npc_agent.remainingDistance <= npc_agent.stoppingDistance) && npc_agent.hasPath)
        {
            return true;
        }
        return false;
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
            Die();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, view_range);
    }

    private void Die()
    {
        int drop_dice = Random.Range(0, 99);

        if (drop_dice < drop_chance && alive)
        {

            Item drop_item = GameManager.instance.GetRandomLoot();

            GameObject new_item = Instantiate(drop_item.item_world_display, transform.position, transform.rotation);
            ItemWorld iw_comp = new_item.GetComponent<ItemWorld>();
            iw_comp.item_data = drop_item;

        }

        alive = false;
        Destroy(gameObject);
    }
}
