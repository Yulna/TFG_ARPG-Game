using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Chambers of Elrankat/StatVariable")]
public class StatVariable : ScriptableObject
{
    [SerializeField]
    protected float base_value;
    private float sum_value;    
    private float mult_value;   
    protected float buffed_value;

    public UnityEvent stat_Change;

    private void OnEnable()
    {
        sum_value = 0;
        mult_value = 1;
    }

    public float Base_value
    {
        get { return base_value; }
        set { base_value = value; stat_Change.Invoke(); }
    }
    public float Sum_value
    {
        get { return sum_value; }
        set { sum_value = value; stat_Change.Invoke(); }
    }
    public float Mult_value
    {
        get { return mult_value; }
        set { mult_value = value; stat_Change.Invoke(); }
    }
    public float Buffed_value
    {
        get { UpdateBuffedValue(); return buffed_value; }
    }



    void UpdateBuffedValue()
    {
        buffed_value = (base_value + sum_value) * mult_value;
    }

}
