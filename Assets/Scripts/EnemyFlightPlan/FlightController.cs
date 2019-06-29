using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour
{
    public GameObject ship;
    private FlightPlan flightPlan;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize FlightPlan
        flightPlan = new FlightPlan();

        // Add instructions
        flightPlan.AddInstructionSetVelocity(5f);
        flightPlan.AddInstructionWait(3f);
        flightPlan.AddInstructionSetVelocity(1f);
        flightPlan.AddInstructionWait(3f);
        flightPlan.AddInstructionSetAngularVelocity(-100f);
        flightPlan.AddInstructionWait(0.8f);
        flightPlan.AddInstructionSetAngularVelocity(0f);
        flightPlan.AddInstructionWait(2f);
        flightPlan.AddInstructionSetVelocity(5f);
        flightPlan.AddInstructionShoot(repeat: false);

        // Unleash hell
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        yield return StartCoroutine(SpawnEnemyWavesSeparatedByTime());
        yield return new WaitForSeconds(2.0f);
        SpawnEnemyWavesSeparatedByDistance();
        yield break;
    }

    private IEnumerator SpawnEnemyWavesSeparatedByTime()
    {
        for (int i=0; i < 10; i++)
        {
            // Create a clone of the ship
            Drivable drivableShip = Instantiate(ship).GetComponent<Drivable>();

            // Create a clone of the flight plan
            FlightPlan plan = new FlightPlan(newInstructions: flightPlan.GetInstructions());

            // Set the ship and execute
            plan.SetShip(drivableShip, position: new Vector2(12, -5), rotation: Quaternion.Euler(x: 0f, y: 180f, z: 0f));
            plan.ExecuteFlightPlan(this);

            // Delay between each enemy
            yield return new WaitForSeconds(1.0f);
        }
        yield break;
    }

    private void SpawnEnemyWavesSeparatedByDistance()
    {
        for (int i = 0; i < 10; i++)
        {
            // Create a clone of the ship
            Drivable drivableShip = Instantiate(ship).GetComponent<Drivable>();

            // Create a clone of the flight plan
            FlightPlan plan = new FlightPlan(newInstructions: flightPlan.GetInstructions());

            // Set the ship and execute
            plan.SetShip(drivableShip, position: new Vector2(12 + i*2, -5), rotation: Quaternion.Euler(x: 0f, y: 180f, z: 0f));
            plan.ExecuteFlightPlan(this);            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
}
