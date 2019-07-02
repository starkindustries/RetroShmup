using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    
    public GameObject ship;
    
    private Transform spawnPoint;
    private FlightPlan flightPlan;
    private FlightController flightController;

    // Start is called before the first frame update
    void Start()
    {
        // Get transform
        spawnPoint = GetComponent<Transform>();

        // Initialize FlightPlan
        flightPlan = new FlightPlan();

        // Add instructions
        flightPlan.AddInstructionSetVelocity(5f);        
        flightPlan.AddInstructionWait(3f);
        flightPlan.AddInstructionSetAngularVelocity(-180f);
        flightPlan.AddInstructionAccelerateForward(2.1f);
        flightPlan.AddInstructionWait(2.1f);
        flightPlan.AddInstructionSetAngularVelocity(0f);
        flightPlan.AddInstructionWait(2f);
        flightPlan.AddInstructionSetVelocity(5f);
        flightPlan.AddInstructionShoot(repeat: false);

        flightController = new FlightController(ship, spawnPoint.position, Quaternion.Euler(0f, 180f, 0f), flightPlan);

        flightController.CloneSpawnEnemyWavesSeparatedByDistance(this, 10, new Vector2(2, 1));
    }
}
