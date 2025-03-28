﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OH.ETL.Core.Utils;

public static class SecurityEncDecryptExtensions
{
    //默认密钥向量 
    private static readonly byte[] Keys = [0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F];

    /// <summary> 
    /// DES加密字符串 
    /// </summary> 
    /// <param name="encryptString">待加密的字符串</param> 
    /// <param name="encryptKey">加密密钥,要求为16位</param> 
    /// <returns>加密成功返回加密后的字符串，失败返回源串</returns> 

    public static string EncryptDES(this string encryptString, string encryptKey = "Key123Ace#321Key")
    {
        try
        {
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey[..16]);
            byte[] rgbIV = Keys;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);

            using var DCSP = Aes.Create();
            using MemoryStream mStream = new();
            using CryptoStream cStream = new(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray()).Replace('+', '_').Replace('/', '~');
        }
        catch (Exception ex)
        {
            throw new Exception("密码加密异常" + ex.Message);
        }

    }

    /// <summary> 
    /// DES解密字符串 
    /// </summary> 
    /// <param name="decryptString">待解密的字符串</param> 
    /// <param name="decryptKey">解密密钥,要求为16位,和加密密钥相同</param> 
    /// <returns>解密成功返回解密后的字符串，失败返源串</returns> 

    public static string DecryptDES(this string decryptString, string decryptKey = "Key123Ace#321Key")
    {
        byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 16));
        byte[] rgbIV = Keys;
        byte[] inputByteArray = Convert.FromBase64String(decryptString.Replace('_', '+').Replace('~', '/'));
        using var DCSP = Aes.Create();
        using MemoryStream mStream = new();
        using CryptoStream cStream = new(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
        byte[] inputByteArrays = new byte[inputByteArray.Length];
        cStream.Write(inputByteArray, 0, inputByteArray.Length);
        cStream.FlushFinalBlock();
        return Encoding.UTF8.GetString(mStream.ToArray());

    }

}
