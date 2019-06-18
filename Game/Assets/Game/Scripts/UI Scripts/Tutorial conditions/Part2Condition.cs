using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part2Condition : TutorialCondition
{
    public override bool ConditionComplete()
    {
        if (Input.GetMouseButtonDown(1) 
            || Input.GetKeyDown(KeyCode.Alpha1)
            || Input.GetKeyDown(KeyCode.Alpha2)
            || Input.GetKeyDown(KeyCode.Alpha3)
            || Input.GetKeyDown(KeyCode.Alpha4))
            return true;
        else
            return false;
    }
}
