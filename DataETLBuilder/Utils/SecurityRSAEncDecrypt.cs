using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DataETLBuilder.Utils;

public class SecurityRSAEncDecrypt
{
    private readonly string privateKey;
    private readonly string publicKey;

    /// <summary>
    /// 适应于Pkcs12证书
    /// </summary>
    /// <param name="certPath"></param>
    /// <param name="mchIdPwd"></param>
    public SecurityRSAEncDecrypt(string certPath, string mchIdPwd)
    {
        var pc = new X509Certificate2(certPath, mchIdPwd, X509KeyStorageFlags.Exportable);
        var keyPair = DotNetUtilities.GetKeyPair(pc.PrivateKey);
        var subjectPublicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(keyPair.Public);
        var privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(keyPair.Private);
        privateKey = Convert.ToBase64String(privateKeyInfo.ParsePrivateKey().GetEncoded());
        publicKey = Convert.ToBase64String(subjectPublicKeyInfo.GetEncoded());
    }

    #region RSA解密
    /// <summary>
    /// RSA解密
    /// </summary>
    /// <param name="decryptstring"></param>
    /// <param name="str_PrivateKey">证书上的密钥</param>
    /// <param name="encodingtype"></param>
    /// <returns></returns>
    public string RSADecrypt(string decryptstring, string encodingtype = " ")
    {
        // 构造函数中 Org.BouncyCastle  会自动转成Pkcs1的密钥
        RSA rsa = CreateRsaFromPrivateKey(privateKey);
        return RSADecrypt(decryptstring, encodingtype, rsa);
    }

    /// <summary>
    /// RSA解密
    /// </summary>
    /// <param name="decryptstring"></param>
    /// <param name="str_PrivateKey">证书上的密钥</param>
    /// <param name="encodingtype"></param>
    /// <returns></returns>
    public static string RSADecrypt(string decryptstring, string str_PrivateKey, string encodingtype = "utf-8")
    {
        //生成对应Pkcs1的密钥
        string privateKey = ConvertPrivateKeyPkcs8ToPcks1(str_PrivateKey);
        RSA rsa = CreateRsaFromPrivateKey(privateKey);
        return RSADecrypt(decryptstring, encodingtype, rsa);
    }

    private static string RSADecrypt(string decryptstring, string encodingtype, RSA rsa)
    {
        int keySize = rsa.KeySize / 8;
        byte[] buffer = new byte[keySize];
        byte[] PlainTextBArray = Convert.FromBase64String(decryptstring);

        using (MemoryStream input = new MemoryStream(PlainTextBArray))
        using (MemoryStream output = new MemoryStream())
        {
            while (true)
            {
                int readLine = input.Read(buffer, 0, keySize);
                if (readLine <= 0)
                {
                    break;
                }
                byte[] temp = new byte[readLine];
                Array.Copy(buffer, 0, temp, 0, readLine);
                byte[] decrypt = rsa.Decrypt(temp, RSAEncryptionPadding.Pkcs1);
                output.Write(decrypt, 0, decrypt.Length);
            }
            rsa.Dispose();
            return Encoding.GetEncoding(encodingtype).GetString(output.ToArray());
        }
    }

    public static string RSAEncrypt(string str_DataToSign, string str_PublicKey, string encodingtype = "utf-8")
    {
        //生成对应Pkcs1的密钥
        string publicKey = ConvertPublicKeyPkcs8ToPcks1(str_PublicKey);
        var rsa = CreateRsaFromPublicKey(str_PublicKey);

        return RSAEncrypt(str_DataToSign, encodingtype, rsa);
    }

    public string RSAEncrypt(string str_DataToSign, string encodingtype = "utf-8")
    {
        // 构造函数中 Org.BouncyCastle  会自动转成Pkcs1的公钥
        var rsa = CreateRsaFromPublicKey(publicKey);
        return RSAEncrypt(str_DataToSign, encodingtype, rsa);
    }

    private static string RSAEncrypt(string str_DataToSign, string encodingtype, RSA rsa)
    {
        var inputBytes = Encoding.GetEncoding(encodingtype).GetBytes(str_DataToSign);//有含义的字符串转化为字节流                              
        int bufferSize = (rsa.KeySize / 8) - 11;//单块最大长度
        var buffer = new byte[bufferSize];
        using MemoryStream inputStream = new(inputBytes),
             outputStream = new();
        while (true)
        { //分段加密
            int readSize = inputStream.Read(buffer, 0, bufferSize);
            if (readSize <= 0)
            {
                break;
            }

            var temp = new byte[readSize];
            Array.Copy(buffer, 0, temp, 0, readSize);
            var encryptedBytes = rsa.Encrypt(temp, RSAEncryptionPadding.Pkcs1);
            outputStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            rsa.Dispose();
        }
        return Convert.ToBase64String(outputStream.ToArray());//转化为字节流方便传输
    }

    private static int GetIntegerSize(BinaryReader binr)
    {
        byte bt = 0;
        int count = 0;
        bt = binr.ReadByte();
        if (bt != 0x02)
            return 0;
        bt = binr.ReadByte();

        if (bt == 0x81)
            count = binr.ReadByte();
        else
        if (bt == 0x82)
        {
            var highbyte = binr.ReadByte();
            var lowbyte = binr.ReadByte();
            byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
            count = BitConverter.ToInt32(modint, 0);
        }
        else
        {
            count = bt;
        }

        while (binr.ReadByte() == 0x00)
        {
            count -= 1;
        }
        binr.BaseStream.Seek(-1, SeekOrigin.Current);
        return count;
    }

    private static bool CompareBytearrays(byte[] a, byte[] b)
    {
        if (a.Length != b.Length)
            return false;
        int i = 0;
        foreach (byte c in a)
        {
            if (c != b[i])
                return false;
            i++;
        }
        return true;
    }
    #endregion

    #region 签名和验签

    /// <summary>
    /// 私钥签名
    /// </summary>
    /// <param name="data"></param>
    /// <param name="str_PrivateKey"></param>
    /// <returns></returns>
    public static string Sign(string data, string str_PrivateKey)
    {
        //生成对应Pkcs1的密钥   Pkcs12,Pkcs8需先转成kcs1
        string privateKey = ConvertPrivateKeyPkcs8ToPcks1(str_PrivateKey);
        RSA rsa = CreateRsaFromPrivateKey(str_PrivateKey);

        byte[] dataBytes = Encoding.UTF8.GetBytes(data);

        var signatureBytes = rsa.SignData(dataBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        return Convert.ToBase64String(signatureBytes);
    }

    /// <summary>
    /// 私钥签名
    /// </summary>
    /// <param name="data"></param>
    /// <param name="str_PrivateKey"></param>
    /// <returns></returns>
    public string Sign(string data)
    {
        RSA rsa = CreateRsaFromPrivateKey(privateKey);
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        var signatureBytes = rsa.SignData(dataBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        return Convert.ToBase64String(signatureBytes);
    }

    /// <summary>
    /// 公钥验签
    /// </summary>
    /// <param name="data">原始数据</param>
    /// <param name="sign">签名</param>
    /// <returns></returns>
    public static bool Verify(string data, string sign, string publicKeyString)
    {
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        byte[] signBytes = Convert.FromBase64String(sign);

        //生成对应Pkcs1的公钥   Pkcs12,Pkcs8需先转成kcs1
        string privateKey = ConvertPublicKeyPkcs8ToPcks1(publicKeyString);
        RSA rsa = CreateRsaFromPublicKey(publicKeyString);
        var verify = rsa.VerifyData(dataBytes, signBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        return verify;
    }

    /// <summary>
    /// 公钥验签
    /// </summary>
    /// <param name="data">原始数据</param>
    /// <param name="sign">签名</param>
    /// <returns></returns>
    public bool Verify(string data, string sign)
    {
        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        byte[] signBytes = Convert.FromBase64String(sign);
        RSA rsa = CreateRsaFromPublicKey(publicKey);
        var verify = rsa.VerifyData(dataBytes, signBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

        return verify;
    }
    #endregion

    #region 生成RSA

    /// <summary>
    /// 根据PrivateKey生成RSA (适用于Pkcs1)
    /// </summary>
    /// <param name="privateKey">证书上的密钥</param>
    /// <returns></returns>
    private static RSA CreateRsaFromPrivateKey(string privateKey)
    {
        var privateKeyBits = System.Convert.FromBase64String(privateKey);
        var rsa = RSA.Create();
        var RSAparams = new RSAParameters();

        using (var binr = new BinaryReader(new MemoryStream(privateKeyBits)))
        {
            byte bt = 0;
            ushort twobytes = 0;
            twobytes = binr.ReadUInt16();
            if (twobytes == 0x8130)
                binr.ReadByte();
            else if (twobytes == 0x8230)
                binr.ReadInt16();
            else
                throw new Exception("Unexpected value read binr.ReadUInt16()");

            twobytes = binr.ReadUInt16();
            if (twobytes != 0x0102)
                throw new Exception("Unexpected version");

            bt = binr.ReadByte();
            if (bt != 0x00)
                throw new Exception("Unexpected value read binr.ReadByte()");

            RSAparams.Modulus = binr.ReadBytes(GetIntegerSize(binr));
            RSAparams.Exponent = binr.ReadBytes(GetIntegerSize(binr));
            RSAparams.D = binr.ReadBytes(GetIntegerSize(binr));
            RSAparams.P = binr.ReadBytes(GetIntegerSize(binr));
            RSAparams.Q = binr.ReadBytes(GetIntegerSize(binr));
            RSAparams.DP = binr.ReadBytes(GetIntegerSize(binr));
            RSAparams.DQ = binr.ReadBytes(GetIntegerSize(binr));
            RSAparams.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
        }
        rsa.ImportParameters(RSAparams);
        return rsa;
    }

    /// <summary>
    /// 根据PublicKey 创建RSA实例
    /// </summary>
    /// <param name="publicKeyString"></param>
    /// <returns></returns>
    public static RSA CreateRsaFromPublicKey(string publicKeyString)
    {
        byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
        byte[] x509key;
        byte[] seq = new byte[15];
        int x509size;

        x509key = Convert.FromBase64String(publicKeyString);
        x509size = x509key.Length;

        using (var mem = new MemoryStream(x509key))
        {
            using (var binr = new BinaryReader(mem))
            {
                byte bt = 0;
                ushort twobytes = 0;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    return null;

                seq = binr.ReadBytes(15);
                if (!CompareBytearrays(seq, SeqOID))
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8103)
                    binr.ReadByte();
                else if (twobytes == 0x8203)
                    binr.ReadInt16();
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    return null;

                twobytes = binr.ReadUInt16();
                byte lowbyte = 0x00;
                byte highbyte = 0x00;

                if (twobytes == 0x8102)
                    lowbyte = binr.ReadByte();
                else if (twobytes == 0x8202)
                {
                    highbyte = binr.ReadByte();
                    lowbyte = binr.ReadByte();
                }
                else
                    return null;
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                int modsize = BitConverter.ToInt32(modint, 0);

                int firstbyte = binr.PeekChar();
                if (firstbyte == 0x00)
                {
                    binr.ReadByte();
                    modsize -= 1;
                }

                byte[] modulus = binr.ReadBytes(modsize);

                if (binr.ReadByte() != 0x02)
                    return null;
                int expbytes = (int)binr.ReadByte();
                byte[] exponent = binr.ReadBytes(expbytes);

                var rsa = RSA.Create();
                var rsaKeyInfo = new RSAParameters
                {
                    Modulus = modulus,
                    Exponent = exponent
                };
                rsa.ImportParameters(rsaKeyInfo);
                return rsa;
            }

        }
    }

    #endregion

    #region pcks8格式转化为pcks1格式

    /// <summary>
    /// 将pcks8格式的RSA公钥转化为pcks1格式
    /// </summary>
    /// <param name="pubicKey">pcks8格式的RSA公钥 base64格式</param>
    /// <returns>pcks1格式的RSA公钥 base64格式</returns>
    public static string ConvertPublicKeyPkcs8ToPcks1(string pubicKey)
    {
        return Convert.ToBase64String(ConvertPublicKeyPkcs8ToPcks1(Convert.FromBase64String(pubicKey)));
    }
    /// <summary>
    /// 将pcks8格式的RSA公钥转化为pcks1格式
    /// </summary>
    /// <param name="pubicKey">pcks8格式的RSA公钥</param>
    /// <returns>pcks1格式的RSA公钥</returns>
    public static byte[] ConvertPublicKeyPkcs8ToPcks1(byte[] pubicKey)
    {
        RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(pubicKey);
        SubjectPublicKeyInfo spkInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(publicKeyParam);
        return spkInfo.ParsePublicKey().ToAsn1Object().GetEncoded();
    }

    /// <summary>
    /// 将pcks8格式的RSA私钥转化为pcks1格式
    /// </summary>
    /// <param name="privateKey">pcks8格式的RSA私钥 base64格式</param>
    /// <returns>pcks1格式的RSA私钥 base64格式</returns>
    public static string ConvertPrivateKeyPkcs8ToPcks1(string privateKey)
    {
        return Convert.ToBase64String(ConvertPrivateKeyPkcs8ToPcks1(Convert.FromBase64String(privateKey)));
    }
    /// <summary>
    /// 将pcks8格式的RSA私钥转化为pcks1格式
    /// </summary>
    /// <param name="privateKey">pcks8格式的RSA私钥</param>
    /// <returns>pcks1格式的RSA私钥</returns>
    public static byte[] ConvertPrivateKeyPkcs8ToPcks1(byte[] privateKey)
    {
        RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(privateKey);
        PrivateKeyInfo pkInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKeyParam);
        return pkInfo.ParsePrivateKey().ToAsn1Object().GetEncoded();
    }

    #endregion
}
