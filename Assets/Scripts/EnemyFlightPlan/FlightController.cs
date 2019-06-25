using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour
{
    public GameObject ship;

    private Drivable drivableShip;
    private List<FlightInstruction> instructions;

    // Start is called before the first frame update
    void Start()
    {
        drivableShip = ship.GetComponent<Drivable>();
        if (drivableShip == null)
        {
            Debug.LogError("ERROR: Drivable ship is null!");
        }

        // flight plan
        instructions = new List<FlightInstruction>();
        instructions.Add(new FlightInstruction(FlightInstruction.Action.SetVelocity, 5f));
        instructions.Add(new FlightInstruction(FlightInstruction.Action.Wait, 3f));
        instructions.Add(new FlightInstruction(FlightInstruction.Action.SetVelocity, 0f));
        instructions.Add(new FlightInstruction(FlightInstruction.Action.Wait, 3f));
        instructions.Add(new FlightInstruction(FlightInstruction.Action.Rotate, 100f));

        StartCoroutine(ExecuteFlightPlan());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator ExecuteFlightPlan()
    {
        Debug.Log("FLightPlan Execute function start");
        foreach (FlightInstruction i in instructions)
        {
            Debug.Log("FLightPlan Execute function foreach");
            switch (i.action)
            {
                case FlightInstruction.Action.Wait:
                    yield return new WaitForSeconds(i.floatParam);
                    break;
                case FlightInstruction.Action.SetVelocity:
                    drivableShip.SetVelocity(velocity: i.floatParam);
                    break;
                case FlightInstruction.Action.Shoot:
                    drivableShip.Shoot(repeat: i.boolParam, delayBetweenShots: i.floatParam);
                    break;
                case FlightInstruction.Action.Rotate:
                    drivableShip.SetAngularVelocity(angularVelocity: i.floatParam);
                    break;
            }
        }
        yield break;
    }
}
