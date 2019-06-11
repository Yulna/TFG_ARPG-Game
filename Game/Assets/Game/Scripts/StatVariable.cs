using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//TODO: Fix PropertyDrawer
[CreateAssetMenu(menuName = "Chambers of Elrankat/StatVariable")]
[System.Serializable]
public class StatVariable : ScriptableObject
{
    public string variable_name;
    [SerializeField]
    private float base_value;
    [SerializeField]
    private float sum_value;
    [SerializeField]
    private float mult_value;
    [SerializeField]
    private float buffed_value;

    public UnityEvent stat_Change;

    private void OnEnable()
    {
        sum_value = 0;
        mult_value = 1;
        UpdateBuffedValue();
    }

    public float Base_value
    {
        get { return base_value; }
        set { base_value = value;  UpdateBuffedValue(); stat_Change.Invoke(); }
    }
    public float Sum_value
    {
        get { return sum_value; }
        set { sum_value = value;  UpdateBuffedValue(); stat_Change.Invoke(); }
    }
    public float Mult_value
    {
        get { return mult_value; }
        set { mult_value = value;  UpdateBuffedValue(); stat_Change.Invoke(); }
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
