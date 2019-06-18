using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part4Condition : TutorialCondition
{
    public bool condition_complete;

    private void Start()
    {
        condition_complete = false;
    }

    public override bool ConditionComplete()
    {
        if (condition_complete)
            return true;
        else
            return false;
    }

    public void CompletePart()
    {
        condition_complete = true;
    }

}
