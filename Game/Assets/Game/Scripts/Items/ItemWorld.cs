using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    public float pick_up_radius = 1;
    public bool to_pick;
    public Transform player_transform;
    public Item item_data;

    // Start is called before the first frame update
    void Start()
    {
        player_transform = CharacterController.instance.GetPlayerTransform();
        //item_data.item_name = "Sword of swordness";
    }

    // Update is called once per frame
    void Update()
    {
        if (to_pick == false)
            return;

        if (Vector3.Distance(player_transform.position, gameObject.transform.position) <= pick_up_radius)
        {
            if (CharacterController.instance.inventory.AddItem(item_data))
            {
                Debug.Log("Item picked");
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(gameObject.transform.position, pick_up_radius);
    }

}
