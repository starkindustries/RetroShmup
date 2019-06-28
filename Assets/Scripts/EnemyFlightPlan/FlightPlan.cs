using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightPlan
{
    private Drivable ship;
    private List<FlightInstruction> instructions;

    // Constructors
    public FlightPlan(Drivable drivable)
    {
        ship = drivable;
        instructions = new List<FlightInstruction>();
    }

    public FlightPlan(Drivable drivable, Vector2 position, Quaternion rotation)
    {
        ship = drivable;
        ship.Initialize(position: position, rotation: rotation);
        instructions = new List<FlightInstruction>();
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

    // Execute Flight Plan: start the coroutine
    public void ExecuteFlightPlan(MonoBehaviour gameObject)
    {        
        gameObject.StartCoroutine(ExecuteFlightPlanCoroutine());
    }

    private IEnumerator ExecuteFlightPlanCoroutine()
    {
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
            }
        }
        yield break;
    }
}       