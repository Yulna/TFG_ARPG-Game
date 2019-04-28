using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicScript : MonoBehaviour
{

    public GameObject item_model;
    public Sprite item_sprite;
    public DamageType dmg_type;

    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            GameObject new_item = Instantiate(item_model, transform.position, transform.rotation);
            ItemWorld iw_comp = new_item.GetComponent<ItemWorld>();
            iw_comp.item_data.item_name = "Item generated %i" + count;
            
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            CharacterController.instance.DamagePlayer(100, dmg_type);
        }
    }


    Item GenerateItem()
    {
        Item ret = new Item();
        ret.item_name = "Item generated % i" + count;
        count++;
        ret.equip_slot_id = EquipSlot.Head;

        //TODO put random buff
        

        return ret;
    }
    
}
