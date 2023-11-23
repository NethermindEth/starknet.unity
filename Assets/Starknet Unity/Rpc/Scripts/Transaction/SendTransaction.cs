using UnityEngine;
using StarkSharp.Rpc;
using StarkSharp.Rpc.Modules.Transactions;
using StarkSharp.Platforms;
using StarkSharp.Connectors;
using StarkSharp.Connectors.Components;
using StarkSharp.Platforms.Unity;
using StarkSharp.Settings;

public class SendTransaction : MonoBehaviour
{
    public void Send(string senderAddress, string contractAddress, string functionName, string[] functionArgs, CairoVersion cairoVersion,
                string maxFee, string chainId, string privateKey, string version)
    {
        TransactionInteraction transactionInteraction
            = new TransactionInteraction(senderAddress, contractAddress, functionName, functionArgs, cairoVersion,
                maxFee, chainId, privateKey, version);

        Platform platform = UnityPlatform.New(Platform.PlatformConnectorType.RPC);
        Connector connector = new Connector(platform);

        Settings.apiurl = PlayerPrefs.GetString("RPCNode");
        connector.SendTransaction(transactionInteraction, OnSendTransactionSuccess, OnSendTransactionError);
    }

    private static void OnSendTransactionSuccess(JsonRpcResponse response)
    {
        Debug.Log("Success: ");
        Debug.Log(response);
    }

    private static void OnSendTransactionError(JsonRpcResponse errorMessage)
    {
        Debug.Log("Error: ");
        // Decode JsonRpcResponse and log
        if (errorMessage != null)
        {
            if (errorMessage.error != null)
            {
                Debug.Log(errorMessage.error.message);
                Debug.Log(errorMessage.error.code);
            }
            else
            {
                Debug.Log("Unknown error occurred.");
            }
        }
        else
        {
            Debug.Log("Error message is null.");
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