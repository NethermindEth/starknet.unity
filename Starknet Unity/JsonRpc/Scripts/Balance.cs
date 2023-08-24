using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System;
using System.Globalization;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public static void BalanceOf(string userAddress, string contractAddress, Action<BigInteger> callback)
    {
        string method = "starknet_call";
        var data = new
        {
            contract_address = contractAddress,
            entry_point_selector = Encode.GetSelector("balanceOf"),
            calldata = new string[] { userAddress }
        };
        object[] parameters = new object[] { data, "latest" };

        Rpc.Instance.StartCoroutine(Rpc.RpcHandler(method, parameters, (result) => {
            var response = result as JsonRpcRequest;
            if (response.result != null)
            {
                var balanceHex = response.result[0];
                var balance = BigInteger.Parse(balanceHex.Substring(2), NumberStyles.HexNumber);
                callback(balance);
            }
            else
            {
                throw new Exception("Error getting balance");
            }
        }));
    }

    public void CheckUserBalance()
    {
        string userAddress = "0x3b2d6f0b442e43c36888111924a2a2b8308836658f803abb76bc39b4b43a305";
        string contractAddress = "0x49d36570d4e46f48e99674bd3fcc84644ddd6b96f7c741b1562b82f9e004dc7";
        BalanceOf(userAddress, contractAddress, CheckUserBalanceCallback);
    }

    public void CheckUserBalanceCallback(BigInteger balance)
    {
        Debug.Log(balance);
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckUserBalance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
