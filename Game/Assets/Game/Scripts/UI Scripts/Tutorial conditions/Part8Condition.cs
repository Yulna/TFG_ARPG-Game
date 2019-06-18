using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part8Condition : TutorialCondition
{
    public override bool ConditionComplete()
    {
        if (Input.GetKeyDown(KeyCode.I))
            return true;
        else
            return false;
    }
}
