using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FlightPath
{
    public static Vector2[] GetFlightPathByIndex(int index, int param)
    {
        switch(index)
        {
            case 0:
                return RTLStraightAcross(y: param);
            default:
                Debug.LogError("Invalid index passed to function GetFlightPathByIndex: " + index);
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
        Debug.Log("Path: " + path.ToString());
        return path;
    }
}
