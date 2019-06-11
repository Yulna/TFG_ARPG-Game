using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    BUFF_STAT_ADD = 0,
    BUFF_STAT_MULT = 1,
    BUFF_SKILL_MOD,
    BUFF_PASSIVE
}

[System.Serializable]
public class Buff 
{
    public Buff(BuffType b_type, float b_magnitude, StatVariable b_variable)
    {
        type = b_type;        
        variable = b_variable;
        if (type == BuffType.BUFF_STAT_ADD)
            magnitude = (int)b_magnitude;
        else
        {
            magnitude = Mathf.Round(b_magnitude * 100.0f) / 100.0f;
            if (magnitude > 1)
                Mathf.Round(magnitude);
        }
    }

    public BuffType type;
    public float magnitude;
    public StatVariable variable;
    [SerializeField]
    private bool active;

    public virtual void EnableBuff()
    {
        if (variable == null || active == true)
            return;

        if(type == BuffType.BUFF_STAT_ADD)
        {
            variable.Sum_value += magnitude;
        }
        if(type == BuffType.BUFF_STAT_MULT)
        {
            variable.Mult_value *= magnitude;
        }
        active = true;
    }

    public virtual void DisableBuff()
    {
        if (variable == null || active == false)
            return;

        if (type == BuffType.BUFF_STAT_ADD)
        {
            variable.Sum_value -= magnitude;
        }
        if (type == BuffType.BUFF_STAT_MULT)
        {
            variable.Mult_value /= magnitude;
        }
        active = false;
    }

    public string GetBuffDescription()
    {
        string ret;

        ret = "Increase " + variable.name + " by ";

        if (type == BuffType.BUFF_STAT_ADD)
        {
            ret += magnitude + " units";
        }
        if (type == BuffType.BUFF_STAT_MULT)
        {
            if((magnitude * 100) > 1)
                ret += Mathf.Round((magnitude * 100) - 100) + "%";
            else
                ret += (magnitude * 100) - 100 + "%";
        }
        return ret;
    }
}
