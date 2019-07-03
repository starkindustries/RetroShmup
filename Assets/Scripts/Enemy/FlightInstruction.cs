using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FlightInstruction
{
    public FlightInstruction(Action newAction, float newFloat = 0f, bool newBool = false)
    {
        action = newAction;
        floatParam = newFloat;
        boolParam = newBool;
    }

    // These actions (other than Wait) coorespond to the Drivable Interface methods.
    public enum Action { Wait, SetVelocity, Shoot, Rotate, AccelerateForward }
    public Action action;
    public float floatParam;
    public bool boolParam;
}
