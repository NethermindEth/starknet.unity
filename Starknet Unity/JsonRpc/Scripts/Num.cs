using System;
using System.Numerics;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Num : MonoBehaviour
{
    public static BigInteger ToBigInt(string value)
    {
        return BigInteger.Parse(value);
    }

    public static bool IsBigInt(object value)
    {
        return value is BigInteger;
    }

    public static string ToHex(string number)
    {
        BigInteger bigInt = ToBigInt(number);
        return "0x" + bigInt.ToString("x");
    }

    public static string ToHex(BigInteger number)
    {
        return "0x" + number.ToString("x");
    }

    public static string ToHex(byte[] bytes)
    {
        return "0x" + BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();
    }

    public static bool IsStringWholeNumber(string value)
    {
        return Regex.IsMatch(value, @"^\d+$");
    }

    public static bool IsHex(string hex)
    {
        return Regex.IsMatch(hex, @"^0x[0-9a-f]*$", RegexOptions.IgnoreCase);
    }

    public static string ToDecimal(string hex)
    {
        if (!IsHex(hex))
        {
            throw new Exception("Not a hex string");
        }

        return ToBigInt(hex).ToString();
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
