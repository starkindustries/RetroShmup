using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard: MonoBehaviour
{
    private int total = 0;   

    public void AddPoints(int points)
    {
        total += points;
        Debug.Log("Points added. Current total: " + total);
    }

    public int GetTotalScore()
    {
        return total;
    }
}
