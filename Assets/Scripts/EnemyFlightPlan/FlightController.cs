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
        // Create a clone of the ship
        GameObject newShip = Instantiate(ship);

        Drivable drivableShip = newShip.GetComponent<Drivable>();
        if (drivableShip == null)
        {
            Debug.LogError("ERROR: Drivable ship is null!");
        }        

        // Initialize Ship 
        flightPlan = new FlightPlan(drivableShip, position: new Vector2(12, -5), rotation: Quaternion.Euler(x: 0f, y: 180f, z: 0f));

        // flight plan
        flightPlan.AddInstructionSetVelocity(5f);
        flightPlan.AddInstructionWait(3f);
        flightPlan.AddInstructionSetVelocity(0.5f);
        flightPlan.AddInstructionWait(3f);
        flightPlan.AddInstructionSetAngularVelocity(-100f);
        flightPlan.AddInstructionWait(1f);
        flightPlan.AddInstructionSetAngularVelocity(0f);
        flightPlan.AddInstructionShoot(repeat: false);

        // run flight plan!
        flightPlan.ExecuteFlightPlan(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
}
