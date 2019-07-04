using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugPanel : MonoBehaviour
{
    public TextMeshProUGUI debugText;

    private Quaternion initialTilt;

    private void Start()
    {
        if (GyroController.Instance.IsEnabled())
        {
            initialTilt = GyroController.Instance.GetAttitude().Value;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        if (GyroController.Instance.IsEnabled())
        { 
            Quaternion attitude = GyroController.Instance.GetAttitude().Value;
            debugText.text =  "gyro: (" + Format(attitude.x) + ", " + Format(attitude.y) + ", " + Format(attitude.z) + ", " + Format(attitude.w) + ")\n";
            debugText.text += "init: (" + Format(initialTilt.x) + ", " + Format(initialTilt.y) + ", " + Format(initialTilt.z) + ", " + Format(initialTilt.w) + ")\n";
            debugText.text += "diff: (" + Format(attitude.x - initialTilt.x) + ", " + Format(attitude.y - initialTilt.y) + ")\n";
        }
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
