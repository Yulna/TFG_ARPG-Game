using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part6Condition : TutorialCondition
{
    public GameObject enemy;

    public override bool ConditionComplete()
    {
        if (enemy == null)
            return true;
        else
            return false;
    }
}
