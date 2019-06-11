using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicScript : MonoBehaviour
{

    public GameObject item_world_prefab;
    public Sprite item_sprite;
    public DamageType dmg_type;

    public Item special_item;

    int count;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;

        special_item.item_buffs[0] = new BuffTripleTornado();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Item drop_item = GameManager.instance.GetRandomLoot();

            GameObject new_item = Instantiate(drop_item.item_world_display, transform.position, transform.rotation);
            ItemWorld iw_comp = new_item.GetComponent<ItemWorld>();
            iw_comp.item_data = drop_item;

        }

        if (Input.GetKeyDown(KeyCode.L))
        {
        
            GameObject new_item = Instantiate(special_item.item_world_display, transform.position, transform.rotation);
            ItemWorld iw_comp = new_item.GetComponent<ItemWorld>();
            iw_comp.item_data = special_item;

        }

        if (Input.GetKeyDown(KeyCode.Space))
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
        ret.item_buffs = new Buff[2];
        
        for(int i = 0; i < ret.item_buffs.Length; i++)
        {
            ret.item_buffs[i] = GetBuff();
        }

        return ret;
    }
    
    Buff GetBuff()
    {
        Buff ret = new Buff(BuffType.BUFF_STAT_ADD,2,CharacterController.instance.variables_stats[(int)StatId.WeaponDmg]);


        return ret;
    }

}
