using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour
{
    private bool gyroEnabled;
    private Gyroscope gyro;

    #region Singleton Pattern
    //*****************
    // Singleton pattern
    // https://gamedev.stackexchange.com/a/116010/123894
    private static GyroController _instance;
    public static GyroController Instance { get { return _instance; } }

    private void Awake()
    {
        // Singleton Enforcement Code
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }

        // Check if Gyroscope is available
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            gyroEnabled = true;
        }
        else
        {
            gyroEnabled = false;
        }
    }
    #endregion
    
    public bool IsEnabled()
    {
        return gyroEnabled;
    }

    public Quaternion? GetAttitude()
    {
        if (gyroEnabled)
        {
            return gyro.attitude;
        }
        return null;
    }
}
