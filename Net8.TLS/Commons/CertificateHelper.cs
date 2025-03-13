using Net8.TLS.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Net8.TLS.Commons;

public class CertificateHelper
{
    /// <summary>
    /// 生成证书发行主题头
    /// </summary>
    /// <param name="issuer">颁发者</param>
    /// <returns></returns>
    private CertIssueHeader GenCertSubject(string issuer)
    {
        var certSubject = new CertIssueHeader()
        {
            CountryName = "CN",
            ProvinceName = "GD",
            LocalityName = "Fs",
            OrganizationName = "Kingser Group LTD",
            OrganizationalUnitName = "ZJMes",

            CommonName = issuer,
            Email = "morliz@live.cn"
        };

        return certSubject;
    }

    /// <summary>
    /// 拼接证书主题头
    /// </summary>
    /// <param name="issuer">颁发者</param>
    /// <returns>字符串</returns>
    private string ConcatCertSubject(string issuer)
    {
        string subject = string.Empty;
        var certSubject = GenCertSubject(issuer);

        var certType = certSubject.GetType();
        foreach (var property in certType.GetProperties())
        {
            var attribute = property.GetCustomAttribute<DescriptionAttribute>();
            var description = attribute != null ? attribute.Description : "No description provided";
            //var propertyName = property.Name;
            var propertyValue = property.GetValue(certSubject)?.ToString();

            subject += $"{description}={propertyValue},";
        }

        return subject.Remove(subject.Length - 1);
    }

    /// <summary>
    /// 构建X.500可分辨名称
    /// </summary>
    /// <param name="commonName">公共证书名称</param>
    /// <returns></returns>
    private X500DistinguishedName BuildX500DistinguishedName(string commonName)
    {
        var builder = new X500DistinguishedNameBuilder();
        builder.AddEmailAddress("morliz@live.cn");
        builder.AddCommonName(commonName);

        builder.AddOrganizationalUnitName("ZJMes");
        builder.AddOrganizationName("Kingser Group LTD");
        builder.AddLocalityName("FouShan");
        builder.AddStateOrProvinceName("GD");
        builder.AddCountryOrRegion("CN");

        return builder.Build();
    }

    /// <summary>
    /// 生成根证书
    /// </summary>
    /// <param name="commonName">公共证书名称</param>
    /// <param name="yearValidPeriod">年限</param>
    /// <returns></returns>
    public X509Certificate2 GenRootCertificate(string commonName, int yearValidPeriod)
    {
        // Generate private-public key pair
        RSA rsaKey = RSA.Create(2048);

        // Describe certificate
        //string commonName = "Kingser RSA Root Certificate Authority";
        var rootX500DistinguishedName = BuildX500DistinguishedName(commonName);

        // Create certificate request
        var certificateRequest = new CertificateRequest(rootX500DistinguishedName, rsaKey,
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1);

        //基本约束(2.5.29.19)
        certificateRequest.CertificateExtensions.Add(
            new X509BasicConstraintsExtension(
                certificateAuthority: true,     //是否root CA证书
                hasPathLengthConstraint: false,  //允许的路径级别数是否有限制
                pathLengthConstraint: 0,    //证书路径允许的级别数
                critical: true      //是否关键扩展
            )
        );

        //使用者密钥标识符(2.5.29.14)
        var ski = new X509SubjectKeyIdentifierExtension(key: certificateRequest.PublicKey, critical: false);
        certificateRequest.CertificateExtensions.Add(ski);

        //授权密钥标识符AKI(2.5.29.35) -NET7 or later ver.
        certificateRequest.CertificateExtensions.Add(
            X509AuthorityKeyIdentifierExtension.CreateFromSubjectKeyIdentifier(ski));

        //可缺省 -NotAfter参数，生成证书时默认有效期为1年
        var expireAt = DateTimeOffset.UtcNow.AddYears(yearValidPeriod).AddDays(-2);

        //Creates a self-signed certificate.
        var certificate = certificateRequest.CreateSelfSigned(DateTimeOffset.UtcNow, expireAt);

        //Create a signature for the certificate.
        //var signaturePen = X509SignatureGenerator.CreateForRSA(rsaKey, RSASignaturePadding.Pkcs1);

        //可缺省-FriendlyName参数，生成证书时[友好名称]为空
        certificate.FriendlyName = commonName;

        return certificate;
    }

    /// <summary>
    /// 生成终端实体证书
    /// </summary>
    /// <param name="parentCert">父级证书(一般为根证书)</param>
    /// <param name="commonName">公共证书名称</param>
    /// <param name="yearValidPeriod">年限</param>
    /// <param name="dnsName">可扩展域名</param>
    /// <returns></returns>
    public X509Certificate2 GenEndentityCertificate(X509Certificate2 parentCert,
            string commonName, int yearValidPeriod, string dnsName, out RSA rsaKey)
    {
        rsaKey = RSA.Create(2048);

        // Describe certificate
        var entityX500DistinguishedName = BuildX500DistinguishedName(commonName);

        // Create certificate request
        var certificateRequest = new CertificateRequest(entityX500DistinguishedName, rsaKey,
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1);

        //基本约束(2.5.29.19)
        certificateRequest.CertificateExtensions.Add(
            new X509BasicConstraintsExtension(
                certificateAuthority: false,
                hasPathLengthConstraint: false,
                pathLengthConstraint: 0,
                critical: false
            )
        );

        /* 密钥用法(2.5.29.15)
         * KeyCertSign：证书签名
         * CRLSign：证书吊销列表 (CRL) 签名
         * DataEncipherment：数据加密
         * DecipherOnly：只能用于解密
         * DigitalSignature：数字签名
         * EncipherOnly：只能用于加密
         * KeyAgreement：用于确定密钥协议
         * KeyEncipherment：用于密钥加密
         * None：无密钥使用参数
         * NonRepudiation：用于身份验证
         */
        certificateRequest.CertificateExtensions.Add(
            new X509KeyUsageExtension(
                keyUsages: X509KeyUsageFlags.DigitalSignature
                | X509KeyUsageFlags.NonRepudiation
                | X509KeyUsageFlags.KeyEncipherment
                | X509KeyUsageFlags.KeyCertSign,
                critical: true
            )
        );

        //使用者可选名称(2.5.29.17)
        if (!string.IsNullOrEmpty(dnsName))
        {
            var sanbuilder = new SubjectAlternativeNameBuilder();
            sanbuilder.AddDnsName(dnsName);
            //sanbuilder.AddIpAddress(IPAddress.Parse("10.0.5.76"));
            certificateRequest.CertificateExtensions.Add(sanbuilder.Build());
        }

        //使用者密钥标识符(2.5.29.14)
        certificateRequest.CertificateExtensions.Add(
            new X509SubjectKeyIdentifierExtension(
                key: certificateRequest.PublicKey,
                critical: false
            )
        );

        //授权密钥标识符AKI(2.5.29.35) -NET7 or later ver.
        var skiExt = parentCert.Extensions.First(ext => ext?.Oid?.Value == "2.5.29.14") as X509SubjectKeyIdentifierExtension;

        certificateRequest.CertificateExtensions.Add(
            X509AuthorityKeyIdentifierExtension.CreateFromSubjectKeyIdentifier(skiExt));


        /* 增强型密钥用法(2.5.29.37)
         * 所有颁发的策略 (2.5.29.32.0)
         * 客户端身份验证 (1.3.6.1.5.5.7.3.2)
         * 服务器身份验证 (1.3.6.1.5.5.7.3.1)
         * BitLocker驱动器加密 (1.3.6.1.4.1.311.67.1.1)
         * 加密文档系统 (1.3.6.1.4.1.311.10.3.4)
         * 文档签名 (1.3.6.1.4.1.311.10.3.12)
         * 安全电子邮件 (1.3.6.1.5.5.7.3.4)
         * 
        certificateRequest.CertificateExtensions.Add(
            new X509EnhancedKeyUsageExtension(
                new OidCollection()
                {
                    new Oid("1.3.6.1.5.5.7.3.2"),
                    new Oid("1.3.6.1.5.5.7.3.1"),
                },
                false));
        */

        //可缺省 -NotAfter参数，生成证书时默认有效期为1年
        var expireAt = DateTimeOffset.UtcNow.AddYears(yearValidPeriod).AddDays(-2);

        var serialNumber = GenerateSerialNumber(8);
        var certificate = certificateRequest.Create(parentCert,
            DateTimeOffset.UtcNow, expireAt, serialNumber);

        //可缺省-FriendlyName参数，生成证书时[友好名称]为空
        certificate.FriendlyName = commonName;

        return certificate;
    }

    private byte[] GenerateSerialNumber(int iLengthInBits)
    {
        byte[] serialNumber = new byte[iLengthInBits];
        var random = new Random();
        random.NextBytes(serialNumber);

        //Console.WriteLine(BitConverter.ToString(serialNumber).Replace("-", ""));
        return serialNumber;
    }

    /// <summary>
    /// 转换成密码
    /// </summary>
    /// <param name="plainCode">明码</param>
    /// <returns></returns>
    public SecureString CoverterCipherCode(string plainCode)
    {
        var CipherCode = new SecureString();
        foreach (var @char in plainCode)
            CipherCode.AppendChar(@char);

        return CipherCode;
    }

    //导出Pfx证书
    public void ExportPfxCertificate(X509Certificate2 certificate, RSA rsaKey, CertStore certStore)
    {
        //certificate.FriendlyName = certStore.Issuer;

        // Create password for certificate protection
        var certFilePwd = CoverterCipherCode(certStore.FilePwd);

        if (certificate.GetRSAPrivateKey() != null)
        {
            // Export certificate to a file
            File.WriteAllBytes($"{certStore.PrimaryFilename}.pfx",
                certificate.Export(X509ContentType.Pfx, certFilePwd)
                );
        }
        else
        {
            // Export certificate with private key
            var exportableCertificate = new X509Certificate2(
                certificate.Export(X509ContentType.Pfx),
                (string)null,
                X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet
            ).CopyWithPrivateKey(rsaKey);
            exportableCertificate.FriendlyName = certStore.Issuer;

            File.WriteAllBytes($"{certStore.PrimaryFilename}.pfx",
                exportableCertificate.Export(X509ContentType.Pfx, certFilePwd));
        }
    }
    //导出Cer证书
    public void ExportCerCertificate(X509Certificate2 certificate, CertStore certStore)
    {
        //certificate.FriendlyName = certStore.Issuer;

        // Export certificate to a file
        File.WriteAllBytes($"{certStore.PrimaryFilename}.cer",
            certificate.Export(X509ContentType.Cert)
            );
    }

    //检查证书是否安装
    private void RemoveOldCertificate(string certSubjectName, StoreName name, StoreLocation location)
    {
        var store = new X509Store(name, location);
        store.Open(OpenFlags.MaxAllowed);
        var certs = store.Certificates.Find(X509FindType.FindBySubjectName, certSubjectName, false);
        //return certs.Count <= 0 || certs[0].NotAfter < DateTime.Now;
        if (certs.Count > 0)
            store.RemoveRange(certs);
    }

    //导入Pfx证书
    public void ImportPfxCertificate(string pfxFile, string pfxPwd,
        StoreName name = StoreName.My, StoreLocation location = StoreLocation.CurrentUser)
    {
        var pfxCert = new X509Certificate2(pfxFile, pfxPwd);
        var store = new X509Store(name, location);
        store.Open(OpenFlags.ReadWrite);

        RemoveOldCertificate(pfxCert.SubjectName.Name, name, location);

        store.Add(pfxCert);
        store.Close();
    }

    //导入Cer证书Bug
    public void ImportCerCertificate(string cerFile, string cerPwd,
    StoreName name = StoreName.My, StoreLocation location = StoreLocation.CurrentUser)
    {
        var cerCert = new X509Certificate2(cerFile, cerPwd);
        var store = new X509Store(name, location);
        store.Open(OpenFlags.ReadWrite);

        var subjectName = cerCert.SubjectName.Name.Split(',')[5].Replace(" CN=", "");
        RemoveOldCertificate(subjectName, name, location);

        store.Add(cerCert);
        store.Close();
    }

}
