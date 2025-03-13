using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Net8.TLS.Entity;

public class CertStore
{
    //public CertIssueHeader Subject { get; set; } = null!;

    [Required]
    public string Issuer { get; set; } = null!;

    [Required]
    public int ValidPeriod { get; set; }

    public string Dnsname { get; set; } = null!;

    public string PrimaryFilename { get; set; } = null!;

    public string FilePwd { get; set; } = null!;

}


/// <summary>
/// 证书发行主题
/// </summary>
public class CertIssueHeader
{
    [Description("E")]
    public string? Email { get; set; }

    [Required]
    [Description("CN")]
    public string CommonName { get; set; } = null!;

    [Description("OU")]
    public string? OrganizationalUnitName { get; set; }
    [Description("O")]
    public string? OrganizationName { get; set; }
    [Description("L")]
    public string? LocalityName { get; set; }
    [Description("S")]
    public string? ProvinceName { get; set; }
    [Description("C")]
    public string? CountryName { get; set; }
}
