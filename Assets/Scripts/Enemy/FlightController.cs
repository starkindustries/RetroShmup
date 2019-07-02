using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController
{
    private GameObject ship;
    private Vector2 position;
    private Quaternion rotation;
    private FlightPlan flightPlan;

    // Must initialize flight controller before it can be useful
    public FlightController(GameObject newShip, Vector2 newPosition, Quaternion newRotation, FlightPlan newPlan)
    {
        ship = newShip;
        position = newPosition;
        rotation = newRotation;
        flightPlan = newPlan;
    }

    // This spawns all clones **at the same location** but separated by time
    public void CloneSpawnEnemyWavesSeparatedByTime(MonoBehaviour gameObject, int cloneCount, float delay)
    {
        gameObject.StartCoroutine(CloneSpawnEnemyWavesSeparatedByTimeHelper(gameObject, cloneCount, delay));
    }

    public IEnumerator CloneSpawnEnemyWavesSeparatedByTimeHelper(MonoBehaviour gameObject, int cloneCount, float delay)
    {
        for (int i = 0; i < cloneCount; i++)
        {
            // Create a clone of the ship
            Drivable drivableShip = MonoBehaviour.Instantiate(ship).GetComponent<Drivable>();

            // Create a clone of the flight plan
            FlightPlan plan = new FlightPlan(newInstructions: flightPlan.GetInstructions());

            // Set the ship and execute
            plan.SetShip(drivableShip, position: position, rotation: rotation);
            plan.ExecuteFlightPlan(gameObject);

            // Delay between each enemy
            yield return new WaitForSeconds(delay);
        }
        yield break;
    }

    // This spawns all clones **at the same time** but separated by the distance vector
    public void CloneSpawnEnemyWavesSeparatedByDistance(MonoBehaviour gameObject, int cloneCount, Vector2 distance)
    {
        for (int i = 0; i < cloneCount; i++)
        {
            // Create a clone of the ship
            Drivable drivableShip = MonoBehaviour.Instantiate(ship).GetComponent<Drivable>();

            // Create a clone of the flight plan
            FlightPlan plan = new FlightPlan(newInstructions: flightPlan.GetInstructions());

            // Set the ship and execute                        
            plan.SetShip(drivableShip, position: position + i * distance, rotation: rotation);
            plan.ExecuteFlightPlan(gameObject);
        }
    }
}
