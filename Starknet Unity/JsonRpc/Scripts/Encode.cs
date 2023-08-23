using System;
using System.Numerics;
using System.Linq;
using UnityEngine;
using Nethereum.Util;

public class Encode : MonoBehaviour
{
    public static string RemoveHexPrefix(string hex)
    {
        return hex.Replace("0x", string.Empty, StringComparison.OrdinalIgnoreCase);
    }

    public static string AddHexPrefix(string hex)
    {
        return "0x" + RemoveHexPrefix(hex);
    }

    private static readonly BigInteger MASK_250 = BigInteger.One << 250; // Shift 1 to the left by 250 bits

    public static string KeccakBn(BigInteger value)
    {
        string hexWithoutPrefix = RemoveHexPrefix(Num.ToHex(value));
        string evenHex = hexWithoutPrefix.Length % 2 == 0 ? hexWithoutPrefix : "0" + hexWithoutPrefix;
        return AddHexPrefix(Num.ToHex(new Sha3Keccack().CalculateHashFromHex(evenHex)));
    }

    public static string KeccakHex(string value)
    {
        return AddHexPrefix(Num.ToHex(new Sha3Keccack().CalculateHash(System.Text.Encoding.UTF8.GetBytes(value))));
    }

    public static BigInteger StarknetKeccak(string value)
    {
        string hexValue = RemoveHexPrefix(KeccakHex(value));

        // Get the last 64 characters (32 bytes) of the hex string
        string last64Chars = hexValue.Length <= 64 ? hexValue : hexValue.Substring(hexValue.Length - 64);

        byte[] bytes = Enumerable.Range(0, last64Chars.Length / 2)
                        .Select(x => Convert.ToByte(last64Chars.Substring(x * 2, 2), 16))
                        .Reverse() // Reverse the bytes to get the correct endian order
                        .ToArray();

        // Ensure the highest-order bit is not set by adding an extra 0 byte if needed
        if ((bytes[bytes.Length - 1] & 0x80) > 0)
        {
            Array.Resize(ref bytes, bytes.Length + 1);
        }

        BigInteger hash = new BigInteger(bytes);

        BigInteger result = hash % MASK_250;
        return result;
    }

    public static string GetSelectorFromName(string funcName)
    {
        return Num.ToHex(StarknetKeccak(funcName));
    }

    public static string GetSelector(string value)
    {
        if (Num.IsHex(value))
        {
            return value;
        }
        if (Num.IsStringWholeNumber(value))
        {
            return ToHexString(value);
        }
        return GetSelectorFromName(value);
    }

    private static string ToHexString(string number) => BigInteger.Parse(number).ToString("x");

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
