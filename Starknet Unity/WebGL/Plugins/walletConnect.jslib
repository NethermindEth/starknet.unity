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

	SendTransactionArgentX: async function(contractAddress, entrypoint, params)
	{
		if (window.starknet_argentX.selectedAddress)
		{
			const response = await window.starknet_argentX.account.execute([{
				contractAddress: contractAddress,
				entrypoint: entrypoint,
				calldata: params
			}]);
			return response.transaction_hash;
		}
	},

	SendTransactionBraavos: async function(contractAddress, entrypoint, params)
	{
		if (window.starknet_braavos.selectedAddress)
		{
			const response = await window.starknet_braavos.account.execute([{
				contractAddress: contractAddress,
				entrypoint: entrypoint,
				calldata: params
			}]);
			return response.transaction_hash;
		}
	},

	SendTransaction: async function(contractAddress, entrypoint, params)
	{
		if (window.starknet.selectedAddress)
		{
			const response = await window.starknet.account.execute([{
				contractAddress: contractAddress,
				entrypoint: entrypoint,
				calldata: params
			}]);
			return response.transaction_hash;
		}
	},

	CallContract: async function(contractAddress, entrypoint, params)
	{
		if (window.starknet.selectedAddress)
		{
			const response = await window.starknet.account.callContract({
				contractAddress: contractAddress,
				entrypoint: entrypoint,
				calldata: params
			});
			return response.result;
		}
	},
});