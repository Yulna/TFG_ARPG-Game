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

public class Buff 
{
    public BuffType type;
    public float magnitude;
    public StatVariable to_buff;
    public bool active;

    public virtual void EnableBuff()
    {
        if (to_buff == null || active == true)
            return;

        if(type == BuffType.BUFF_STAT_ADD)
        {
            to_buff.Sum_value += magnitude;
        }
        if(type == BuffType.BUFF_STAT_MULT)
        {
            to_buff.Mult_value *= magnitude;
        }
        active = true;
    }

    public virtual void DisableBuff()
    {
        if (to_buff == null || active == false)
            return;

        if (type == BuffType.BUFF_STAT_ADD)
        {
            to_buff.Sum_value -= magnitude;
        }
        if (type == BuffType.BUFF_STAT_MULT)
        {
            to_buff.Mult_value /= magnitude;
        }
        active = false;
    }
}
