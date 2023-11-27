using UnityEditor;
using UnityEngine;

public class SDKSetupWindow : EditorWindow
{
    string rpcNode;
    private string[] options = new string[] { "Dojo", "Starknet" };
    private int selectedIndex;
    private string worldAddress;
    private string actionAddress;

    [MenuItem("Starknet SDK/Setup")]
    public static void ShowWindow()
    {
        GetWindow<SDKSetupWindow>("Starknet SDK Setup");
    }

    void OnEnable()
    {
        // Load saved values when the window is opened or re-focused
        rpcNode = EditorPrefs.GetString("RPCNode", "Enter RPC Node");
        selectedIndex = EditorPrefs.GetInt("SelectedIndex", 0);
        worldAddress = EditorPrefs.GetString("WorldAddress", "Enter World Address");
        actionAddress = EditorPrefs.GetString("ActionAddress", "Enter Action Address");
    }

    void OnGUI()
    {
        GUILayout.Label("Setup your Starknet SDK", EditorStyles.boldLabel);
        selectedIndex = EditorGUILayout.Popup("Game Engine", selectedIndex, options);

        rpcNode = EditorGUILayout.TextField("RPC Node", rpcNode);

        if (selectedIndex == 0)
        {
            worldAddress = EditorGUILayout.TextField("World address", worldAddress);
            actionAddress = EditorGUILayout.TextField("Action address", actionAddress);
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
        SaveSettingsToFile();

        // Save values to EditorPrefs so they persist between sessions
        EditorPrefs.SetString("RPCNode", rpcNode);
        EditorPrefs.SetInt("SelectedIndex", selectedIndex);
        EditorPrefs.SetString("WorldAddress", worldAddress);
        EditorPrefs.SetString("ActionAddress", actionAddress);
    }

    void SaveSettingsToFile()
    {
        RuntimeSDKInitializer.SDKSettings settings = new RuntimeSDKInitializer.SDKSettings { rpcNode = rpcNode, gameEngine = options[selectedIndex], worldAddress = worldAddress, actionAddress = actionAddress };
        string json = JsonUtility.ToJson(settings);
        System.IO.File.WriteAllText(Application.dataPath + "/Resources/SDKSettings.json", json);
    }
}
