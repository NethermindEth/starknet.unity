using System;
using System.Collections;
using UnityEngine;

public class WalletConnect : MonoBehaviour
{
    public static string userAddress;
    private IEnumerator ConnectWalletAsync(Action connectWalletFunction)
    {
        // Call the JavaScript method to connect the wallet
        connectWalletFunction();

        // Wait for the connection to be established
        yield return new WaitUntil(() => JSInteropManager.IsConnected());

        userAddress = JSInteropManager.GetAccount();
        Debug.Log("Connected to wallet: " + userAddress);
    }

    public void OnButtonConnectWalletArgentX()
    {
        StartCoroutine(ConnectWalletAsync(JSInteropManager.ConnectWalletArgentX));
    }

    public void OnButtonConnectWalletBraavos()
    {
        StartCoroutine(ConnectWalletAsync(JSInteropManager.ConnectWalletBraavos));
    }

    // Start is called before the first frame update
    void Start()
    {
        if (JSInteropManager.IsConnected())
        {
            userAddress = JSInteropManager.GetAccount();
            Debug.Log("Connected to wallet: " + userAddress);
        }
        bool available = JSInteropManager.IsWalletAvailable();
        if (!available)
        {
            JSInteropManager.AskToInstallWallet();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
