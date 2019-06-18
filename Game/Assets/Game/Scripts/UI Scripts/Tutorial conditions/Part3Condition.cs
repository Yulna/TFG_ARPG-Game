using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part3Condition : TutorialCondition
{
    public override bool ConditionComplete()
    {
        if (Input.GetKeyDown(KeyCode.S))
            return true;
        else
            return false;
    }
}
