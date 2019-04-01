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
    LMC,
    RMC
};

public class SkillController : MonoBehaviour
{
   

    public GameObject skill_selection_UI;
    int selected_id;

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
        equip_skills[(int)SkillButton.LMC] = (Skill)ScriptableObject.CreateInstance("AttackMelee");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.S))
        {
            //Enable/Disable Skill selection UI
            skill_selection_UI.SetActive(!skill_selection_UI.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && equip_skills[(int)SkillButton.NUM_2] != null)
        {
          //  RayHitInfo hit_info = PlayerController.instance.HandleCameraRay(Input.mousePosition, LayerMask.GetMask("Enemy", "Floor"));
          //  equip_skills[(int)SkillButton.NUM_2].CastSkill(transform.position, hit_info.hit_point);            
        }

    }

    public void CastSkill(SkillButton skill_index, RayHitInfo hit_info)
    {
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


}
