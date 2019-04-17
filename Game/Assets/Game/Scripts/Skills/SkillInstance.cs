using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInstance : MonoBehaviour
{
    public delegate void SkillBehaviour(ref CastInfo cast_info, GameObject display);
    SkillBehaviour curr_behaviour;
    SkillBehaviour base_behaviour;
    CastInfo cast_info;


    void Update()
    {
        if (curr_behaviour == null)
            return;
        else
            curr_behaviour(ref cast_info, gameObject);
    }

    public void InitInstance(SkillBehaviour new_behaviour, CastInfo new_cast_info)
    {
        if (curr_behaviour == null && base_behaviour == null)
        {
            curr_behaviour = new_behaviour;
            base_behaviour = new_behaviour;
            cast_info = new_cast_info;
        }
        else
            Debug.LogError("skillbehvaiour already initiated");
    }



    //Behaviour changes
    public void AddBehaviour(SkillBehaviour new_behaviour)
    {
        curr_behaviour += new_behaviour;
    }

    public void RemoveBehaviour(SkillBehaviour old_behaviour)
    {
        curr_behaviour -= old_behaviour;
    }

    public void ReplaceBehaviour(SkillBehaviour new_behaviour)
    {
        curr_behaviour = new_behaviour;
    }

    public void ResetBehaviour()
    {
        curr_behaviour = base_behaviour;
    }
}
