using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarkSharp.Rpc;
using StarkSharp.Rpc.Utils;
using StarkSharp.StarkCurve.Signature;
using StarkSharp.Platforms.Unity.RPC;
using StarkSharp.Connectors.Components;
using StarkSharp.Settings;

public class Invoke : MonoBehaviour
{
    public class Request
    {
        public string type { get; set; }
        public string sender_address { get; set; }
        public string[] calldata { get; set; }
        public string max_fee { get; set; }
        public string version { get; set; }
        public string[] signature { get; set; }
        public string nonce { get; set; }
    }

    private static string maxFee;
    private static string chainId;
    private static string privateKey;
    private static string senderAddress;
    private static string contractAddress;
    private static string calldataHash;
    private static string[] calldata;


    public void CreateTransaction(string _senderAddress, string _contractAddress, string functionName, string[] functionArgs, int cairoVersion, string _maxFee, string _chainId, string _privateKey)
    {
        maxFee = _maxFee;
        chainId = _chainId;
        privateKey = _privateKey;
        senderAddress = _senderAddress;
        contractAddress = _contractAddress;

        string functionNameSelector = StarknetOps.CalculateFunctionSelector(functionName);
        TransactionHash.Call[] callArray = new TransactionHash.Call[] { new TransactionHash.Call { To = contractAddress, Selector = functionNameSelector, Data = functionArgs } };
        string _calldataHash = TransactionHash.Hash.ComputeCalldataHash(callArray, cairoVersion);
        string[] _calldata = TransactionHash.Hash.FormatCalldata(callArray, cairoVersion);

        calldataHash = _calldataHash;
        calldata = _calldata;

        string[]
        request = new string[]
        {
            "latest",
            senderAddress
        };

        JsonRpc requestData = JsonRpcHandler.GenerateRequestData("starknet_getNonce", request);
        Debug.Log(requestData);

        StartCoroutine(UnityRpcPlatform.SendPostRequestUnity(requestData, OnNonceComplete));
    }

    private void OnNonceComplete(object response)
    {
        Debug.Log("Nonce complete: " + response);
        object nonce = ((JsonRpcResponse)response).result;
        ECDSA.ECSignature signature = TransactionHash.Hash.SignInvokeTransaction(
                "0x1",
                senderAddress,
                calldataHash,
                maxFee,
                chainId,
                nonce.ToString(),
                TransactionHash.Hash.HexToBigInteger(privateKey)
            );

        string r = TransactionHash.Hash.BigIntegerToHex(signature.R);
        string s = TransactionHash.Hash.BigIntegerToHex(signature.S);

        Request request = new Request
        {
            type = "INVOKE",
            sender_address = senderAddress,
            calldata = calldata,
            max_fee = maxFee,
            version = "0x1",
            signature = new string[] { r, s },
            nonce = nonce.ToString()
        };
        JsonRpc requestData = JsonRpcHandler.GenerateRequestData("starknet_addInvokeTransaction", request);

        StartCoroutine(UnityRpcPlatform.SendPostRequestUnity(requestData, OnTransactionComplete));
    }

    private static void OnTransactionComplete(object result)
    {
        Debug.Log("Transaction complete: " + result);
    }
}
