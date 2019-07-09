using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyWave : MonoBehaviour, Activatable
{    
    public GameObject ship;
    public int enemyCount;
    public Vector2 separationDistance;
    public float waveDelay;
    public List<FlightInstruction> instructions;

    private FlightController flightController;

    public void Activate()
    {
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(waveDelay);
        flightController = new FlightController(ship, transform.position, Quaternion.Euler(transform.rotation.eulerAngles), instructions);
        flightController.CloneSpawnEnemyWavesSeparatedByDistance(this, enemyCount, separationDistance);
    }
}
