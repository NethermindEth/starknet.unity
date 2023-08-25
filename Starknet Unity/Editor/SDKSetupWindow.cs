using UnityEditor;
using UnityEngine;

public class SDKSetupWindow : EditorWindow
{
    string rpcNode = "Enter RPC Node";

    [MenuItem("Starknet SDK/Setup")]
    public static void ShowWindow()
    {
        GetWindow<SDKSetupWindow>("Starknet SDK Setup");
    }

    void OnGUI()
    {
        GUILayout.Label("Setup your Starknet SDK", EditorStyles.boldLabel);

        rpcNode = EditorGUILayout.TextField("RPC Node", rpcNode);

        if (GUILayout.Button("Submit"))
        {
            SetupSDK(rpcNode);
            // Hide the window
            this.Close();
        }
    }

    void SetupSDK(string rpc)
    {
        PlayerPrefs.SetString("RPCNode", rpc);
    }
}
