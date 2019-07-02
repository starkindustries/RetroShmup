using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPlan
{
    private Drivable ship;
    private List<FlightInstruction> instructions;

    // Constructors
    public FlightPlan()
    {
        instructions = new List<FlightInstruction>();
    }

    public FlightPlan(Drivable drivable)
    {
        ship = drivable;
        instructions = new List<FlightInstruction>();
    }

    public FlightPlan(List<FlightInstruction> newInstructions)
    {
        instructions = newInstructions;
    }

    public FlightPlan(Drivable drivable, Vector2 position, Quaternion rotation)
    {
        ship = drivable;
        ship.Initialize(position: position, rotation: rotation);
        instructions = new List<FlightInstruction>();
    }

    // Getters & Setters
    public void SetShip(Drivable drivableShip, Vector2 position, Quaternion rotation)
    {
        ship = drivableShip;
        ship.Initialize(position: position, rotation: rotation);
    }

    public List<FlightInstruction> GetInstructions()
    {
        return instructions;
    }

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

    // Execute Flight Plan: start the coroutine
    public void ExecuteFlightPlan(MonoBehaviour gameObject)
    {        
        gameObject.StartCoroutine(ExecuteFlightPlanCoroutine());
    }

    private IEnumerator ExecuteFlightPlanCoroutine()
    {
        // Check if ship is null
        if (ship == null)
        {
            Debug.LogError("FlightPlan Error: ship is null.");
            yield break;
        }

        // Execute the flight plan!
        Debug.Log("FlightPlan Execute function start!!");
        foreach (FlightInstruction i in instructions)
        {
            if(!ship.IsDrivable())
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
                    ship.SetVelocity(velocity: i.floatParam);
                    break;
                case FlightInstruction.Action.Shoot:
                    ship.Shoot(continuous: i.boolParam, fireRate: i.floatParam);
                    break;
                case FlightInstruction.Action.Rotate:
                    ship.SetAngularVelocity(angularVelocity: i.floatParam);
                    break;
                case FlightInstruction.Action.AccelerateForward:
                    ship.AccelerateForward(timeInSeconds: i.floatParam);
                    break;
            }
        }
        yield break;
    }
}       