using UnityEngine;
using MoonSharp.Interpreter;
using System.IO;

public class LuaEnvironment : MonoBehaviour
{
    private Script environment;
    [SerializeField]
    private string fileName;

    // Start is called before the first frame update
    void Start()
    {
        // Set default options BEFORE you create a new script!
        Script.DefaultOptions.DebugPrint = (s) => Debug.Log(s);
        environment = new Script();                
        // environment.DoString("print 'Hello world!'");
        LoadFile(fileName);
    }

    private void LoadFile(string fileName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        using(BufferedStream stream = new BufferedStream(new FileStream(filePath, FileMode.Open, FileAccess.Read)))
        {
            environment.DoStream(stream);
        }
    }
}
