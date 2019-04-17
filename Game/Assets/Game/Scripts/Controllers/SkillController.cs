using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
}

public class SkillController : MonoBehaviour
{
    public SkillData test;

    public GameObject skill_selection_UI;
    int selected_id;
    public StatVariable attack_speed;

    [Header("Currently equipped skills")]
    public Skill[] equip_skills;
    public Image[] equip_icons;

    [Header("All character skills")]
    public Skill[] char_skill_list;
    public Image[] skill_icons;


    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<char_skill_list.Length; i++)
        {
            skill_icons[i].color = Color.white;
            skill_icons[i].sprite = char_skill_list[i].skill_icon;
        }
        equip_skills[(int)SkillButton.LMB] = (Skill)ScriptableObject.CreateInstance("AttackMelee");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.S))
        {
            //Enable/Disable Skill selection UI
            skill_selection_UI.SetActive(!skill_selection_UI.activeSelf);
        }
        //TODO: Update equiped skills for CD

        //TESt region
        //TODO: remove it
        if(Input.GetKeyDown(KeyCode.P))
        {
            test.AddCastBehaviour(SkillCastMod);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            test.RemoveCastBehaviour(SkillCastMod);
        }
    }

    public void CastSkill(SkillButton skill_index, RayHitInfo hit_info)
    {
        //TODO: Check if we are in the middle of casting a skill
        CharacterController.instance.SpendResource(equip_skills[(int)skill_index].cost);

        equip_skills[(int)skill_index].CastSkill(transform.position, hit_info.hit_point); 
    }


    public void SetSelected(int id)
    {
        selected_id = id;
    }

    public void ChangeSkill(int new_skill_id)
    {
        equip_skills[selected_id] = char_skill_list[new_skill_id];
        equip_icons[selected_id].sprite = equip_skills[selected_id].skill_icon;
    }

    public CastInfo GetCastInfo(Vector3 origin, Vector3 dest)
    {
        CastInfo ret = new CastInfo();

        ret.origin_pos = origin;
        ret.end_pos = dest;
        ret.dir = ret.end_pos - ret.origin_pos;
        ret.dir.Normalize();
        ret.curr_dist = 0;

        return ret;
    }

    public void TestingTEst(RayHitInfo hit)
    {
        test.CastSkill(GetCastInfo(gameObject.transform.position, hit.hit_point));
    }

    //TODO: remove
    public void SkillCastMod(CastInfo cast_info)
    {
        GameObject display = Instantiate(test.skill_display, cast_info.origin_pos, Quaternion.identity);
        SkillInstance instance = display.AddComponent<SkillInstance>();
        cast_info.dir = cast_info.dir * -1.0f;
        instance.InitInstance(test.SkillBehaviour, cast_info);
    }
}
