using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    BUFF_STAT_ADD,
    BUFF_STAT_MULT,
    BUFF_SKILL_MOD,
    BUFF_PASSIVE
}

[System.Serializable]
public class Buff 
{
    public Buff(BuffType b_type, float b_magnitude, StatVariable b_variable)
    {
        type = b_type;
        magnitude = b_magnitude;
        variable = b_variable;
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
}
