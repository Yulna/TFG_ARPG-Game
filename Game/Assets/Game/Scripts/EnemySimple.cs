using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class EnemySimple : MonoBehaviour
{
    public float health;
    public float base_damage;
    public float damage_mult;
    public DamageType dmg_type;
    public float base_attack_range;
    public float base_attack_speed;
    public float view_range;
    public float memory_time;
    public float attack_timer;
    public float move_speed;
    public GameObject attack_particle;

    Animator npc_animator;
    NavMeshAgent npc_agent;
    Transform pc_transform;

    public int drop_chance;

    public GameObject death_particle;
    public GameObject hit_praticle;

    [SerializeField]
    float memory_timer;
    [SerializeField]
    bool player_found;

    private bool alive = true;

    //push variables
    private bool being_pushed = false;
    private float push_force;
    private Vector3 push_dest;
    public float push_precision;
    public float push_speed;

    //Stun variables
    private bool stunned;
    private float stun_duration;

    //Slow variables
    private bool slowed;
    private float slow_duration;
    private float slow_percentile;

    private void Start()
    {
        stunned = false;
        push_force = 1.0f;
        being_pushed = false;
        alive = true;

        npc_animator = GetComponent<Animator>();
        npc_agent = GetComponent<NavMeshAgent>();
        player_found = false;
        pc_transform = CharacterController.instance.move_controller.transform;

        npc_agent.speed = move_speed;
        npc_animator.SetInteger("Action", 1);
    }

    void Update()
    {
        if (CharacterController.instance.main_menu_canvas.activeSelf)
            return;

        if (being_pushed)
            PushSelf();
        
        if(stunned)
        {
            stun_duration -= Time.deltaTime;
            if (stun_duration <= 0)
                stunned = false;
            return;
        }
        if(slowed)
        {
            slow_duration -= Time.deltaTime;
            if (slow_duration <= 0)
                EndSlow();
        }

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
                Vector2 positions = Random.insideUnitCircle * 5;
                npc_agent.SetDestination(transform.position + new Vector3(positions.x,0,positions.y));
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

                if (Vector3.Distance(pc_transform.position,transform.position) <= base_attack_range && attack_timer <= 0)
                {
                    //DO ATTACK
                    attack_timer = 1 / base_attack_speed;

                    Collider[] hit_colliders = Physics.OverlapSphere(transform.position, base_attack_range, LayerMask.GetMask("Player"));

                    for(int i = 0; i < hit_colliders.Length; i++)
                    {
                        npc_animator.SetTrigger("AttackTrigger");
                        Vector3 player_dir = hit_colliders[i].gameObject.transform.position - transform.position;
                        player_dir.Normalize();
                        transform.rotation = Quaternion.LookRotation(player_dir, Vector3.up);
                        float player_angle = Vector3.Angle(player_dir, transform.forward);
                        if (player_angle < 45f)
                        {
                            CharacterController.instance.DamagePlayer(base_damage * damage_mult, dmg_type);
                            Destroy(Instantiate(attack_particle, transform.position + Vector3.up + player_dir, Quaternion.identity), 5);
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

    public void Hurt(float value)
    {
        Debug.Log("enemy damaged");
        if (value > health)
        {
            health = 0;
        }
        else
        {
            health -= value;
        }

        Destroy(Instantiate(hit_praticle, transform.position + Vector3.up, Quaternion.identity), 5);
        if (health <= 0)
            Die();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(gameObject.transform.position, view_range);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, base_attack_range);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, push_dest);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(push_dest, push_precision);

        //Gizmos.color = Color.cyan;
        //Gizmos.DrawWireSphere(npc_agent.destination, 1);
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
        GameObject death_boom = Instantiate(death_particle, transform.position + Vector3.up, Quaternion.Euler(-90,180,0));
        death_boom.transform.localScale = new Vector3(2, 2, 2);
        Destroy(death_boom, 5);
        Destroy(gameObject);
    }

    private void PushSelf()
    {
        if (Vector3.Distance(transform.position, push_dest) > push_precision)
        {
            Vector3 push_dir = push_dest - transform.position;
            push_dir.Normalize();
            GetComponent<NavMeshAgent>().Warp(transform.position + (push_dir * Time.deltaTime * push_force * push_speed));
        }
        else
        {
            being_pushed = false;
        }
    }

    public void PushTowards(Vector3 dir, float force)
    {
        being_pushed = true;
        push_force = force;
        push_dest = transform.position + dir * force;
    }

    public void Stun(float duration)
    {
        stunned = true;
        stun_duration = duration;
    }

    public void ApplySlow(float percentile, float duration)
    {
        //Return if enemy has a bigger slow
        if (slowed && percentile < slow_percentile)
            return;

        if (percentile > 1)
            slow_percentile = 1;
        else
            slow_percentile = percentile;

        npc_agent.speed = move_speed * (1 - slow_percentile); 
        slow_duration = duration;
        slowed = true;
    }
    public void EndSlow()
    {
        npc_agent.speed = move_speed;
        slowed = false;
    }
}
