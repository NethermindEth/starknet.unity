using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

using StarkSharp.Rpc;
using StarkSharp.Platforms;
using StarkSharp.Platforms.Unity.RPC;
using StarkSharp.Settings;
using Newtonsoft.Json.Linq;

public class TransactionStatus : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void GetTransactionStatus(string transactionHash, Action<bool> callback = null)
    {
        Settings.apiurl = PlayerPrefs.GetString("RPCNode");
        UnityRpcPlatform rpcPlatform = new UnityRpcPlatform();

        var requestData = new JsonRpc
        {
            id = 1,
            method = "starknet_getTransactionStatus",
            @params = new object[]
            {
                transactionHash
            }
        };

        rpcPlatform.PlatformRequest(requestData, rpcresponse => OnGetTransactionStatus(rpcresponse, callback));
    }

    private static void OnGetTransactionStatus(JsonRpcResponse response, Action<bool> callback = null)
    {
        var result = response.result;
        if (result != null)
        {
            var data = ((JObject)result).ToObject<Dictionary<string, object>>();
            var transactionStatus = data["execution_status"].ToString();
            if (transactionStatus == "SUCCEEDED")
            {
                Debug.Log($"Transaction {transactionStatus}.");
                callback?.Invoke(true);
            }
            else
            {
                Debug.Log($"Transaction {transactionStatus}.");
                callback?.Invoke(false);
            }
        }
        else
        {
            Debug.Log("Unknown transaction status.");
            callback?.Invoke(true);
        }
    }
}

