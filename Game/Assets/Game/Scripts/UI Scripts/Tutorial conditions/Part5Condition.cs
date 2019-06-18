using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part5Condition : TutorialCondition
{
    public override bool ConditionComplete()
    {
        if (Input.GetKeyDown(KeyCode.C))
            return true;
        else
            return false;
    }
}
