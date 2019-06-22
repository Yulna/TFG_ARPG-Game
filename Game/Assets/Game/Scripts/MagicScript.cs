using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicScript : MonoBehaviour
{

    public GameObject fps_counter;

    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Item drop_item = GameManager.instance.GetRandomLoot();

            GameObject new_item = Instantiate(drop_item.item_world_display, CharacterController.instance.GetPlayerTransform().position + Vector3.up, transform.rotation);
            ItemWorld iw_comp = new_item.GetComponent<ItemWorld>();
            iw_comp.item_data = drop_item;
        }
        
        if(Input.GetKeyDown(KeyCode.F4))
        {
            fps_counter.SetActive(!fps_counter.activeSelf);
        }

    }

}
