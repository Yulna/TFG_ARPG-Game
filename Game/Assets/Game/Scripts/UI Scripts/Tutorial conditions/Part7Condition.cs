using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part7Condition : TutorialCondition
{
    public override bool ConditionComplete()
    {
        if (Input.GetMouseButton(0))
            return true;
        else
            return false;
    }
}
