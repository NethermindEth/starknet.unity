# Getting Started

## Install a Starknet Wallet

You will need to install a Starknet wallet in order to develop a Starknet-enabled game. We recommend using the [Argent wallet](https://www.argent.xyz/) or the [Braavos wallet](https://braavos.app/).

## Download the Starknet Unity SDK

In order to install `starknet.unity` into your Unity project, you must first download the `starknet.unitypackage` package. You can download the [latest release](https://github.com/NethermindEth/starknet.unity/releases) directly from this [download link](https://github.com/NethermindEth/starknet.unity/releases/latest/download/starknet.unitypackage).

![install starknet.unity](assets/install.png)

## Create a Unity Project

Create a new Unity project or open an existing one. We recommend using Unity 2021.3.0 or later.

![create unity project](assets/create-project.png)

## Import the Starknet Unity SDK

Import the `starknet.unitypackage` package into your Unity project. You can do this manually through `Assets -> Import Package -> Custom Package` or by dragging it into the `Assets` folder in Unity editor.

![import starknet.unity](assets/import-package.png)

Make sure you import all of the files in the package.

![import starknet.unity](assets/import-all.png)

After importing the package, you should see a popup dialogue that prompts you to select your preferred game engine and enter your RPC URL. If you are building on Dojo, you'll also be asked to enter the world address.

In case you don't see this dialogue, you can always open it manually by going to `Starknet SDK -> Setup`.

![rpc url](assets/rpc-node.png)

## Import Newtonsoft.JSON

Newtonsoft’s Json.NET Package is required to use the SDK successfully.

Go to `Windows -> Package Manager`. Once the Package Manager window opens, go to `Add package from git URL`, type `com.unity.nuget.newtonsoft-json` then click **Add**.

## Connect to a Starknet Wallet

You can now enable players to connect to their Starknet wallets. Get started with the ConnectWallet sample scene by going to `Starknet Unity -> Scenes -> ConnectWallet`.

![connect wallet](assets/connect-wallet.png)

## Build and Run

Go to the build settings and switch the platform to WebGL. Then, go to Player Settings and uncheck the Auto Graphics API in `Other Settings`. This will make sure that the WebGL build will use WebGL 2.0.

![webgl](assets/switch-platform.png)

Select the appropriate WebGL template as shown below.

![webgl](assets/webgl-template.png)

Finally, build and run the project. You should now be able to connect to a Starknet wallet.

![webgl](assets/deploy.png)
