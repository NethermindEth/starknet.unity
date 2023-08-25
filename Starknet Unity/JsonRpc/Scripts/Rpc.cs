using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class Rpc : MonoBehaviour
{
    public static Rpc Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static IEnumerator RpcHandler(string method, object[] parameters, Action<object> callback)
    {
        var request = new JsonRpcRequest
        {
            Method = method,
            Params = parameters,
            Id = 0
        };

        var json = JsonConvert.SerializeObject(request);
        var content = new System.Text.UTF8Encoding().GetBytes(json);
        string RPC_URL = PlayerPrefs.GetString("RPCNode");

        UnityWebRequest www = new UnityWebRequest(RPC_URL, "POST");
        www.uploadHandler = new UploadHandlerRaw(content);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(www.error);
        }
        else
        {
            var responseString = www.downloadHandler.text;
            var responseJson = JsonConvert.DeserializeObject<JsonRpcRequest>(responseString);
            callback(responseJson);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class JsonRpcRequest
{
    public string JsonRpc { get; set; } = "2.0";
    public string Method { get; set; }
    public object[] Params { get; set; }
    public int Id { get; set; }
    public string[] result { get; set; }
}
