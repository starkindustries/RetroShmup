using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightInstruction
{
    public FlightInstruction(Action newAction, float newFloat = 0f, bool newBool = false)
    {
        action = newAction;
        floatParam = newFloat;
        boolParam = newBool;
    }

    public enum Action { Wait, SetVelocity, Shoot, Rotate }
    public Action action;
    public float floatParam;
    public bool boolParam;
}
