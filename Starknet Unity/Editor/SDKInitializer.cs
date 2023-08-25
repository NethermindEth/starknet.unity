using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class SDKInitializer
{
    static SDKInitializer()
    {
        // Check if SDK needs setup
        if (NeedsSetup())
        {
            SDKSetupWindow.ShowWindow();
        }
    }

    static bool NeedsSetup()
    {
        return !PlayerPrefs.HasKey("RPCNode");
    }
}
