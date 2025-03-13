using Net8.TLS.Commons;
using Net8.TLS.Entity;
using System.Net.Security;
using System.Runtime.ConstrainedExecution;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

namespace Net8.TLS;

internal class Program
{
    static void Main(string[] args)
    {
        //服务端证书
        /*
        var serverCer = new X509Certificate2("serverCert.pfx", "Kissgh_");
        var pem = new StringBuilder();
        var base64 = Convert.ToBase64String(serverCer.Export(X509ContentType.Cert));

        pem.AppendLine("-----BEGIN CERTIFICATE-----");
        pem.AppendLine(base64);
        pem.AppendLine("-----END CERTIFICATE-----");

        string pemCertificateChain = pem.ToString();
        ValidateCertificateChain(pemCertificateChain);
        */

        /*
         * 
         * RevocationMode:检查 X509 证书吊销的模式[Online:在线证书检查]
         * RevocationFlag:设置 X509 吊销标志的值[ExcludeRoot:非根证书外整个链吊销检查]
         * VerificationFlags:指定链中证书的验证条件[NoFlag:不含任何与验证相关的标志]
         * VerificationTime:验证链所用的时间
         * UrlRetrievalTimeout:设置联机吊销验证或下载证书吊销列表 (CRL) 期间所用的最长时间
         */

        var rootCert = new X509Certificate2("rootCert.cer");
        var serverCert = new X509Certificate2("serverCert.cer");
        var clientCert = new X509Certificate2("clientCert.cer");

        X509Chain chain = new();
        //chain.ChainPolicy.ExtraStore.Add(rootCert);
        chain.ChainPolicy.CustomTrustStore.Add(rootCert);
        chain.ChainPolicy.ExtraStore.Add(serverCert);
        //chain.ChainPolicy.VerificationFlags = X509VerificationFlags.AllowUnknownCertificateAuthority;
        chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
        chain.ChainPolicy.TrustMode = X509ChainTrustMode.CustomRootTrust;

        var success = chain.Build(clientCert);
        // 输出链信息
        Console.WriteLine("Chain built. Is valid: " + success);
        foreach (X509ChainStatus error in chain.ChainStatus)
        {
            Console.WriteLine($"{error.Status}: {error.StatusInformation}");
        }

        Console.WriteLine("Hello, World!");
        Console.ReadKey();
    }

    private static void CreateCertificate()
    {
        var CertificateExt = new CertificateHelper();

        //root certificate.
        var rootStore = new CertStore()
        {
            Issuer = "Kingser RSA Root Certificate Authority 2024",
            ValidPeriod = 20,
            PrimaryFilename = "root",
            FilePwd = "Kissgh_"
        };
        var rootCert = CertificateExt.GenRootCertificate(rootStore.Issuer, rootStore.ValidPeriod);

        // Import root certificate to root region.
        //var store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
        //store.Open(OpenFlags.ReadWrite);
        //store.Add(rootCertificate);

        // Export root certificate to file.
        //var certFilePwd = CertificateExt.CoverterCipherCode(rootStore.FilePwd);
        //File.WriteAllBytes($"{rootStore.PrimaryFilename}.pfx",
        //    rootCert.Export(X509ContentType.Pfx, certFilePwd));
        CertificateExt.ExportPfxCertificate(rootCert, null, rootStore);
        CertificateExt.ExportCerCertificate(rootCert, rootStore);

        //server certificate.
        var srvStore = new CertStore()
        {
            Issuer = "MesWebSrv",
            ValidPeriod = 10,
            Dnsname = "*.mes.com",
            PrimaryFilename = "server",
            FilePwd = "Kissgh_"
        };
        var srvCert = CertificateExt.GenEndentityCertificate(rootCert, srvStore.Issuer, srvStore.ValidPeriod,
            srvStore.Dnsname, out RSA srvKey);
        // Export root certificate to file.
        CertificateExt.ExportPfxCertificate(srvCert, srvKey, srvStore);
        CertificateExt.ExportCerCertificate(srvCert, srvStore);

        //client certificate.
        var cieStore = new CertStore()
        {
            Issuer = "MesWebCie",
            ValidPeriod = 3,
            Dnsname = "",
            PrimaryFilename = "client",
            FilePwd = "Kissgh_"
        };
        var cieCert = CertificateExt.GenEndentityCertificate(rootCert, cieStore.Issuer, cieStore.ValidPeriod,
            cieStore.Dnsname, out RSA cieKey);
        // Export root certificate to file.
        CertificateExt.ExportPfxCertificate(cieCert, cieKey, cieStore);
        CertificateExt.ExportCerCertificate(cieCert, cieStore);
    }

    public static void ValidateCertificateChain()
    {
        X509Chain chain = new()
        {
            ChainPolicy = new()
            {
                RevocationMode = X509RevocationMode.NoCheck,
                RevocationFlag = X509RevocationFlag.ExcludeRoot,
                VerificationFlags = X509VerificationFlags.NoFlag,
                VerificationTime = DateTime.Now,
                UrlRetrievalTimeout = new TimeSpan(0, 1, 0)
            }
        };

        var store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        store.Open(OpenFlags.MaxAllowed);
        var certs = store.Certificates.Find(X509FindType.FindBySubjectName, "MesWebCie", false).First();

        //chain.Build(certs);
        if (!chain.Build(certs))
        {
            Console.WriteLine("Invalid certificate chain");
        }

        // 输出链信息
        Console.WriteLine("Chain built. Is valid: " + chain.Build(certs));
        foreach (X509ChainStatus status in chain.ChainStatus)
        {
            Console.WriteLine($"Chain status: {status.StatusInformation}");
        }
    }

    private static bool ValidateCertificateChain(HttpRequestMessage request, X509Certificate2 certificate,
    X509Chain? chain,
    SslPolicyErrors errors)
    {
        if (errors != SslPolicyErrors.None)
            Console.WriteLine($"error:{errors}");

        //var issuer = certificate.Issuer.Split(',')
        //    .Where(element => element.StartsWith(" CN=")).First().Replace(" CN=", "");
        //Console.WriteLine($"The parent CA certificate issuer name is {issuer}.");

        // Custom validation logic.
        var success = chain.Build(certificate);

        // 输出链信息
        Console.WriteLine("Chain built. Is valid: " + success);
        if (chain.ChainStatus.Length > 0)
            foreach (X509ChainStatus error in chain.ChainStatus)
                Console.WriteLine($"{error.Status}: {error.StatusInformation}");

        return success;
    }

    private async void ClientLoginAuthentication()
    {
        //Open WebApi interface.
        var handler = new HttpClientHandler
        {
            SslProtocols = SslProtocols.Tls12,
            ServerCertificateCustomValidationCallback = ValidateCertificateChain
        };

        //add client certificate.
        var clientCert = new X509Certificate2(Path.Combine(Directory.GetCurrentDirectory(),
            "clientCert.pfx"), "Kissgh_");
        handler.ClientCertificates.Add(clientCert);

        var client = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://webapi.mes.com:9991")
        };
        var response = await client.GetAsync("/WeatherForecast");
        var content = await response.Content.ReadAsStringAsync();

        Console.WriteLine(content);
    }

    private static void GetX509Extension(string subjectName)
    {
        X509Store store = new(StoreName.My, StoreLocation.CurrentUser);
        store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

        //X509Certificate2Collection collection = store.Certificates;
        var rootCer = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, false).First();
        foreach (var extension in rootCer.Extensions)
        {
            Console.WriteLine(extension.Oid.FriendlyName + "(" + extension.Oid.Value + ")");

            if (extension.Oid.FriendlyName == "密钥用法")
            {
                X509KeyUsageExtension ext = (X509KeyUsageExtension)extension;
                Console.WriteLine(ext.KeyUsages);
            }

            if (extension.Oid.FriendlyName == "基本约束")
            {
                X509BasicConstraintsExtension ext = (X509BasicConstraintsExtension)extension;
                Console.WriteLine(ext.CertificateAuthority);
                Console.WriteLine(ext.HasPathLengthConstraint);
                Console.WriteLine(ext.PathLengthConstraint);
            }

            if (extension.Oid.FriendlyName == "使用者密钥标识符")
            {
                X509SubjectKeyIdentifierExtension ext = (X509SubjectKeyIdentifierExtension)extension;
                Console.WriteLine(ext.SubjectKeyIdentifier);
            }

            if (extension.Oid.FriendlyName == "增强型密钥用法")
            {
                X509EnhancedKeyUsageExtension ext = (X509EnhancedKeyUsageExtension)extension;
                OidCollection oids = ext.EnhancedKeyUsages;
                foreach (Oid oid in oids)
                {
                    Console.WriteLine(oid.FriendlyName + "(" + oid.Value + ")");
                }
            }

            if (extension.Oid.FriendlyName == "授权密钥标识符")
            {
                X509AuthorityKeyIdentifierExtension ext = (X509AuthorityKeyIdentifierExtension)extension;
                var akiBytes = ext.RawData;
                var akiString = BitConverter.ToString(akiBytes).Replace("-", "");

                Console.WriteLine($"Authority Key Identifier:{akiString}");

                var body = ext.KeyIdentifier.GetValueOrDefault();
                Console.WriteLine(Encoding.UTF8.GetString(body.Span));
            }
        }
        store.Close();

    }
}
