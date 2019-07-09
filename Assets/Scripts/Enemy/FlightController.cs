using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController
{
    private GameObject ship;
    private Vector2 position;
    private Quaternion rotation;
    private List<FlightInstruction> instructions;

    // Must initialize flight controller before it can be useful
    public FlightController(GameObject newShip, Vector2 newPosition, Quaternion newRotation, List<FlightInstruction> newInstructions)
    {
        ship = newShip;
        position = newPosition;
        rotation = newRotation;
        instructions = newInstructions;
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
            Drivable shipInterface = MonoBehaviour.Instantiate(ship).GetComponent<Drivable>();

            // Set the ship and execute
            shipInterface.Initialize(position: position, rotation: rotation);
            gameObject.StartCoroutine(ExecuteFlightInstructions(shipInterface));            

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
            Drivable shipInterface = MonoBehaviour.Instantiate(ship).GetComponent<Drivable>();

            // Set the ship and execute         
            shipInterface.Initialize(position: position + i * distance, rotation: rotation);
            gameObject.StartCoroutine(ExecuteFlightInstructions(shipInterface));
        }
    }

    private IEnumerator ExecuteFlightInstructions(Drivable shipInterface)
    {
        // Check if ship is null
        if (shipInterface == null)
        {
            Debug.LogError("FlightPlan Error: ship is null.");
            yield break;
        }

        // Execute the flight plan!
        Debug.Log("FlightPlan Execute function start!!");
        foreach (FlightInstruction i in instructions)
        {
            if (!shipInterface.IsDrivable())
            {
                break;
            }
            Debug.Log("FlightPlan Execute function foreach");
            switch (i.action)
            {
                case FlightInstruction.Action.Wait:
                    yield return new WaitForSeconds(i.floatParam);
                    break;
                case FlightInstruction.Action.SetVelocity:
                    shipInterface.SetVelocity(velocity: i.floatParam);
                    break;
                case FlightInstruction.Action.Shoot:
                    shipInterface.Shoot(continuous: i.boolParam, fireRate: i.floatParam);
                    break;
                case FlightInstruction.Action.Rotate:
                    shipInterface.SetAngularVelocity(angularVelocity: i.floatParam);
                    break;
                case FlightInstruction.Action.AccelerateForward:
                    shipInterface.AccelerateForward(timeInSeconds: i.floatParam);
                    break;
            }
        }
        yield break;
    }

    #region Add Instruction Functions
    // Add Instruction: Set Velocity
    public void AddInstructionSetVelocity(float velocity)
    {
        instructions.Add(new FlightInstruction(FlightInstruction.Action.SetVelocity, velocity));
    }

    // Add Instruction: Wait
    public void AddInstructionWait(float delay)
    {
        instructions.Add(new FlightInstruction(FlightInstruction.Action.Wait, delay));
    }

    // Add Instruction: Set Angular Velocity
    public void AddInstructionSetAngularVelocity(float angularVelocity)
    {
        instructions.Add(new FlightInstruction(FlightInstruction.Action.Rotate, angularVelocity));
    }

    // Add Instruction: Shoot
    public void AddInstructionShoot(bool repeat, float delayBetweenShots = 0f)
    {
        instructions.Add(new FlightInstruction(FlightInstruction.Action.Shoot, delayBetweenShots, repeat));
    }

    // Add Instruction Accelerate Forward
    // The intent for this function is to allow the enemy to update its forward movement within its update function
    public void AddInstructionAccelerateForward(float timeInSeconds)
    {
        instructions.Add(new FlightInstruction(FlightInstruction.Action.AccelerateForward, timeInSeconds));
    }
    #endregion
}
