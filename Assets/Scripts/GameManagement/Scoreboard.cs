using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard: MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private int total = 0;

    private void Start()
    {
        scoreText.text = "0";
    }

    public void AddPoints(int points)
    {
        total += points;
        Debug.Log("Points added. Current total: " + total);
        scoreText.text = total.ToString();
    }

    public int GetTotalScore()
    {
        return total;
    }
}
