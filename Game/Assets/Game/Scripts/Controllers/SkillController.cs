using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum SkillButton
{
    NUM_1,
    NUM_2,
    NUM_3,
    NUM_4,
    LMB,
    RMC
};

public struct CastInfo
{
    //Casting info --> Need to be set for each skill casted
    public Vector3 origin_pos;
    public Vector3 end_pos;
    public Vector3 dir;
    public float curr_dist;
    public float curr_duration;
    public List<Collider> hitted_colliders;
}

public class SkillController : MonoBehaviour
{
    public SkillData test;

    public GameObject skill_selection_UI;
    int selected_id;
    public StatVariable attack_speed;
    public float cast_timer;

    [Header("Currently equipped skills")]
    public SkillData[] equip_skills;
    public Image[] equip_icons;

    [Header("All character skills")]
    public SkillData[] char_skill_list;
    public Image[] skill_icons;


    public UnityEvent skill_change;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < char_skill_list.Length; i++)
        {
            skill_icons[i].color = Color.white;
            skill_icons[i].sprite = char_skill_list[i].skill_icon;
        }
        //    equip_skills[(int)SkillButton.LMB] = (SkillData)ScriptableObject.CreateInstance("AttackMelee");

        for (int i = 0; i < equip_skills.Length; i++)
        {
            if (equip_skills[i] != null)
                equip_icons[i].sprite = equip_skills[i].skill_icon;
        }
        //Call to update UI
        skill_change.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        cast_timer -= Time.deltaTime;
        if (Input.GetKeyUp(KeyCode.S))
        {
            //Enable/Disable Skill selection UI
            skill_selection_UI.SetActive(!skill_selection_UI.activeSelf);
            CharacterController.instance.ui_open = skill_selection_UI.activeSelf;
        }
        //TODO: Update equiped skills for CD
        for(int i = 0; i < 6; i++)
        {
            if (equip_skills[i].cd_timer > 0)
                equip_skills[i].cd_timer -= Time.deltaTime;
        }
    }

    public void CastSkill(SkillButton skill_index, RayHitInfo hit_info)
    {
        if(equip_skills[(int)skill_index].IsOnCooldown())
        {
            Debug.Log("Skill is on cd");
            return;
        }

        if (cast_timer > 0)
        {
            Debug.Log("Already casting a skill");
        }
        else
        {
            if (equip_skills[(int)skill_index].cast_time_mult == 0)
                cast_timer = 0;
            else
                cast_timer = 1 / (equip_skills[(int)skill_index].cast_time_mult * attack_speed.Buffed_value);

            if (CharacterController.instance.SpendResource(equip_skills[(int)skill_index].cost))
            {
                CharacterController.instance.move_controller.StopMovement();
                equip_skills[(int)skill_index].CastSkill(GetCastInfo(transform.position, hit_info.hit_point));
                transform.rotation = Quaternion.LookRotation(hit_info.hit_point - (transform.position + Vector3.up * 1.5f), Vector3.up);
            }
        }
       
    }

    public void SetSelected(int id)
    {
        selected_id = id;
    }

    public void ChangeSkill(int new_skill_id)
    {
        equip_skills[selected_id] = char_skill_list[new_skill_id];
        equip_icons[selected_id].sprite = equip_skills[selected_id].skill_icon;
        skill_change.Invoke();
    }

    public CastInfo GetCastInfo(Vector3 origin, Vector3 dest)
    {
        CastInfo ret = new CastInfo();

        ret.origin_pos = origin;
        ret.end_pos = dest;
        ret.dir = ret.end_pos - ret.origin_pos;
        ret.dir.Normalize();
        ret.curr_dist = 0;
        ret.curr_duration = 0;

        return ret;
    }

    public Sprite GetSpriteFromButton(SkillButton button_id)
    {
        if (equip_skills[(int)button_id] != null)
            return equip_skills[(int)button_id].skill_icon;
        else
            return null;
    }
        
    public float GetSkillCDDisplayPercentile(SkillButton button_id)
    {
        return equip_skills[(int)button_id].GetCDPercentile();
    }


    public string GetSkillNameFromSkillbar(SkillButton button_id)
    {
        if (equip_skills[(int)button_id] != null)
            return equip_skills[(int)button_id].GetName();
        else
            return "";
    }
    public string GetSkillCostFromSkillbar(SkillButton button_id)
    {
        if (equip_skills[(int)button_id] != null)
            return equip_skills[(int)button_id].GetCostString();
        else
            return "";
    }
    public string GetSkillDescriptionFromSkillbar(SkillButton button_id)
    {
        if (equip_skills[(int)button_id] != null)
            return equip_skills[(int)button_id].GetDescription();
        else
            return "";
    }

    public string GetSkillNameFromSkillList(int list_id)
    {
        if (char_skill_list[list_id] != null)
            return char_skill_list[list_id].GetName();
        else
            return "";
    }
    public string GetSkillCostFromSkillList(int list_id)
    {
        if (char_skill_list[list_id] != null)
            return char_skill_list[list_id].GetCostString();
        else
            return "";
    }
    public string GetSkillDescriptionFromSkillList(int list_id)
    {
        if (char_skill_list[list_id] != null)
            return char_skill_list[list_id].GetDescription();
        else
            return "";
    }

    public SkillData GetSkillByName(string skill_name)
    {
        for (int i = 0; i < char_skill_list.Length; i++)
        {
            if (char_skill_list[i].name == skill_name)
                return char_skill_list[i];
        }
        return null;
    }
}
