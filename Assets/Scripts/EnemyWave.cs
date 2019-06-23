using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    public new string name;
    public GameObject enemy;
    public FlightPathName flightPath;
    public int flightParam = 0;
    public int enemyCount;
    public float delayBeforeWave;
    public float delayBetweenEnemies;
    public float delayAfterWave;    
}
