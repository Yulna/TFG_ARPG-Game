using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part1Condition : TutorialCondition
{
    public override bool ConditionComplete()
    {
        if (Input.GetMouseButtonDown(0))
            return true;
        else
            return false;
    }
}
