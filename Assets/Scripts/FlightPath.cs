using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum FlightPathName
{
    RTLStraightAcross,
    RTLLoopFromBelow,
    RTLLoopFromAbove
}

public static class FlightPath
{
    public static Vector2[] GetFlightPath(FlightPathName flightPath, int param)
    {
        switch(flightPath)
        {
            case FlightPathName.RTLStraightAcross:
                return RTLStraightAcross(y: param);
            case FlightPathName.RTLLoopFromBelow:
                return RTLLoopFromBelow();
            case FlightPathName.RTLLoopFromAbove:
                return RTLLoopFromAbove();
            default:
                Debug.LogError("Invalid flight path GetFlightPath: " + flightPath.ToString());
                return new Vector2[2];
        }
    }
    
    // RTL stands for Right To Left
    // Accepts a y value from -5 to 5, which are within screen bounds
    public static Vector2[] RTLStraightAcross(int y)
    {
        y = Mathf.Clamp(y, -5, 5);
        Vector2[] path =
        {
            new Vector2(12, y),
            new Vector2(-12, y)
        };               
        return path;
    }

    public static Vector2[] RTLLoopFromBelow()
    {
        Vector2[] path =
        {
            new Vector2(12, -5),
            new Vector2(0, -5),
            new Vector2(0, 5),
            new Vector2(0, -3),
            new Vector2(-12, -5)
        };
        return path;
    }

    public static Vector2[] RTLLoopFromAbove()
    {
        Vector2[] path =
        {
            new Vector2(12, 5),
            new Vector2(0, 5),
            new Vector2(0, -5),
            new Vector2(0, 3),
            new Vector2(-12, 5)
        };
        return path;
    }
}
