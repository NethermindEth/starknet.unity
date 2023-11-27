using UnityEngine;
using System.IO;

public class RuntimeSDKInitializer
{
    [System.Serializable]
    public class SDKSettings
    {
        public string rpcNode;
        public string gameEngine;
        public string worldAddress;
        public string actionAddress;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeOnStart()
    {
        Debug.Log("Initializing SDK");
        TextAsset settingsAsset = Resources.Load<TextAsset>("SDKSettings");
        if (settingsAsset != null)
        {
            string json = settingsAsset.text;
            SDKSettings settings = JsonUtility.FromJson<SDKSettings>(json);
            InitializeSDK(settings);
        }
        else
        {
            Debug.LogError("SDK settings file not found");
        }
    }

    private static void InitializeSDK(SDKSettings settings)
    {
        PlayerPrefs.SetString("RPCNode", settings.rpcNode);
        PlayerPrefs.SetString("Game Engine", settings.gameEngine);
        PlayerPrefs.SetString("World Address", settings.worldAddress);
        PlayerPrefs.SetString("Action Address", settings.actionAddress);
    }
}
