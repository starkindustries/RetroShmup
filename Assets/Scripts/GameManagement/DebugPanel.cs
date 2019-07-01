using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugPanel : MonoBehaviour
{
    public TextMeshProUGUI debugText;

    private Vector3 initialTilt;

    private void Start()
    {
        initialTilt = Input.acceleration;
    }

    // Update is called once per frame
    void Update()
    {        
        debugText.text  = "  accel: (" + Format(Input.acceleration.x) + ", " + Format(Input.acceleration.y) + ", " + Format(Input.acceleration.z) + ")\n";
        debugText.text += "initial: (" + Format(initialTilt.x) + ", " + Format(initialTilt.y) + ", " + Format(initialTilt.z) + ")\n";
        debugText.text += "   diff: (" + Format(Input.acceleration.x - initialTilt.x) + ", " + Format(Input.acceleration.y - initialTilt.y) + ", " + Format(Input.acceleration.z - initialTilt.z) + ")\n";
    }

    // Custom String Format
    private string Format(float n)
    {
        // Round number to two decimal places
        float rounded = Mathf.Round(n * 10f) / 10f;

        // Keep trailing zeros (for consistent spacing)
        string roundedString = rounded.ToString("0.0");

        // Check if negative to account for extra "-" character
        if (rounded < 0)
        {
            return roundedString;
        }        
        else
        {
            return " " + roundedString;
        }
    }
}
