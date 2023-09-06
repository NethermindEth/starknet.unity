using UnityEditor;
using UnityEngine;

public class SDKSetupWindow : EditorWindow
{
    string rpcNode = "Enter RPC Node";
    private string[] options = new string[] { "Dojo", "Starknet" }; // Add or remove options as needed.
    private int selectedIndex = 0;
    private string worldAddress = "Enter World Address";

    [MenuItem("Starknet SDK/Setup")]
    public static void ShowWindow()
    {
        GetWindow<SDKSetupWindow>("Starknet SDK Setup");
    }

    void OnGUI()
    {
        GUILayout.Label("Setup your Starknet SDK", EditorStyles.boldLabel);
        selectedIndex = EditorGUILayout.Popup("Game Engine", selectedIndex, options);

        rpcNode = EditorGUILayout.TextField("RPC Node", rpcNode);

        if (selectedIndex == 0)
        {
            EditorGUILayout.TextField("World address", worldAddress);
        }

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
        PlayerPrefs.SetString("Game Engine", options[selectedIndex]);
        PlayerPrefs.SetString("World Address", worldAddress);
    }
}
