mergeInto(LibraryManager.library,
{
	IsWalletAvailable: function()
	{
		if (window.starknet)
		{
			return true;
		}
		else
		{
			return false;
		}
	},

	AskToInstallWallet: function()
	{
		window.alert("Please install Starknet Wallet");
	},

	ConnectWalletArgentX: async function()
	{
		if (window.starknet_argentX)
		{
			await window.starknet_argentX.enable();
		}
	},

	ConnectWalletBraavos: async function()
	{
		if (window.starknet_braavos)
		{
			await window.starknet_braavos.enable();
		}
	},

	IsConnected: function()
	{
		return window.starknet && window.starknet.isConnected;
	},

	GetAccount: function()
	{
		const address = window.starknet.account.address;
		var bufferSize = lengthBytesUTF8(address) + 1;
		var buffer = _malloc(bufferSize);
		stringToUTF8(address, buffer, bufferSize);
		return buffer;
	},

	SendTransactionArgentX: async function(contractAddress, entrypoint, calldata, callbackObjectName, callbackMethodName)
	{
		const jsStringToWasm = (str) => {
			var bufferSize = lengthBytesUTF8(str) + 1;
			var buffer = _malloc(bufferSize);
			stringToUTF8(str, buffer, bufferSize);
			return buffer;
		}
		
		const calldataArray = JSON.parse(UTF8ToString(calldata))
		const contractAddressStr = UTF8ToString(contractAddress)
		const entrypointStr = UTF8ToString(entrypoint)
		const callbackObjectStr = UTF8ToString(callbackObjectName)
		const callbackMethodStr = UTF8ToString(callbackMethodName)

		await window.starknet_argentX.enable();
		if (window.starknet_argentX.selectedAddress)
		{
			window.starknet_argentX.account.execute([{
				contractAddress: contractAddressStr,
				entrypoint: entrypointStr,
				calldata: calldataArray.array
			}]).then((response) => {
				const transactionHash = response.transaction_hash;
				myGameInstance.SendMessage(callbackObjectStr, callbackMethodStr, transactionHash);
			}).catch((error) => {
				const errorMessage = error.message;
				myGameInstance.SendMessage(callbackObjectStr, callbackMethodStr, errorMessage);
			})	
		}
	},

	SendTransactionBraavos: async function(contractAddress, entrypoint, calldata, callbackObjectName, callbackMethodName)
	{
		const jsStringToWasm = (str) => {
			var bufferSize = lengthBytesUTF8(str) + 1;
			var buffer = _malloc(bufferSize);
			stringToUTF8(str, buffer, bufferSize);
			return buffer;
		}
		
		const calldataArray = JSON.parse(UTF8ToString(calldata))
		const contractAddressStr = UTF8ToString(contractAddress)
		const entrypointStr = UTF8ToString(entrypoint)
		const callbackObjectStr = UTF8ToString(callbackObjectName)
		const callbackMethodStr = UTF8ToString(callbackMethodName)

		await window.starknet_braavos.enable();
		if (window.starknet_braavos.selectedAddress)
		{
			window.starknet_braavos.account.execute([{
				contractAddress: contractAddressStr,
				entrypoint: entrypointStr,
				calldata: calldataArray.array
			}]).then((response) => {
				const transactionHash = response.transaction_hash;
				myGameInstance.SendMessage(callbackObjectStr, callbackMethodStr, transactionHash);
			}).catch((error) => {
				const errorMessage = error.message;
				myGameInstance.SendMessage(callbackObjectStr, callbackMethodStr, errorMessage);
			})	
		}
	},

	SendTransaction: async function(contractAddress, entrypoint, calldata, callbackObjectName, callbackMethodName)
	{
		const jsStringToWasm = (str) => {
			var bufferSize = lengthBytesUTF8(str) + 1;
			var buffer = _malloc(bufferSize);
			stringToUTF8(str, buffer, bufferSize);
			return buffer;
		}
		
		const calldataArray = JSON.parse(UTF8ToString(calldata))
		const contractAddressStr = UTF8ToString(contractAddress)
		const entrypointStr = UTF8ToString(entrypoint)
		const callbackObjectStr = UTF8ToString(callbackObjectName)
		const callbackMethodStr = UTF8ToString(callbackMethodName)

		await window.starknet.enable();
		if (window.starknet.selectedAddress)
		{
			window.starknet.account.execute([{
				contractAddress: contractAddressStr,
				entrypoint: entrypointStr,
				calldata: calldataArray.array
			}]).then((response) => {
				const transactionHash = response.transaction_hash;
				myGameInstance.SendMessage(callbackObjectStr, callbackMethodStr, transactionHash);
			}).catch((error) => {
				const errorMessage = error.message;
				myGameInstance.SendMessage(callbackObjectStr, callbackMethodStr, errorMessage);
			})
		}
	},

	CallContract: async function(contractAddress, entrypoint, calldata, callbackObjectName, callbackMethodName)
	{
		const jsStringToWasm = (str) => {
			var bufferSize = lengthBytesUTF8(str) + 1;
			var buffer = _malloc(bufferSize);
			stringToUTF8(str, buffer, bufferSize);
			return buffer;
		}

		const calldataArray = JSON.parse(UTF8ToString(calldata))
		const contractAddressStr = UTF8ToString(contractAddress)
		const entrypointStr = UTF8ToString(entrypoint)
		const callbackObjectStr = UTF8ToString(callbackObjectName)
		const callbackMethodStr = UTF8ToString(callbackMethodName)
		
		await window.starknet.enable();
		if (window.starknet.selectedAddress)
		{
			window.starknet.account.callContract({
				contractAddress: contractAddressStr,
				entrypoint: entrypointStr,
				calldata: calldataArray.array
			}).then((response) => {
				const responseStr = JSON.stringify(response);
				myGameInstance.SendMessage(callbackObjectStr, callbackMethodStr, responseStr);
			}).catch((error) => {
				const errorMessage = error.message;
				myGameInstance.SendMessage(callbackObjectStr, callbackMethodStr, errorMessage);
			})
		}
	},

	FreeWasmString: function(ptr) {
    _free(ptr);
}
});