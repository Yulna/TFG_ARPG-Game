using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part9Condition : TutorialCondition
{
    public override bool ConditionComplete()
    {
        if (Input.GetMouseButtonDown(1) || (Input.GetMouseButtonDown(0) && Input.GetKeyDown(KeyCode.LeftControl)))
            return true;
        else
            return false;
    }
}
