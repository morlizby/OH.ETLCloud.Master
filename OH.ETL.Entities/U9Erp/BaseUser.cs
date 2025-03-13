namespace OH.ETL.Entities.U9Erp;

public partial class BaseUser
{
    public long Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string ModifiedBy { get; set; }

    public long? SysVersion { get; set; }

    public int? Style { get; set; }

    public string Password { get; set; }

    public string TipAnswer { get; set; }

    public string Tip { get; set; }

    public int? PasswordState { get; set; }

    public string PublicKey { get; set; }

    public long LoginLanguage { get; set; }

    public bool? IsAlive { get; set; }

    public long? UserGroup { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public bool? EffectiveIsEffective { get; set; }

    public DateTime? EffectiveEffectiveDate { get; set; }

    public DateTime? EffectiveDisableDate { get; set; }

    public long? Contact { get; set; }

    public long? DateFormat { get; set; }

    public long? TimeFormat { get; set; }

    public long? NumberFormat { get; set; }

    public long? CurrencyFormat { get; set; }

    public string CustomerCode { get; set; }

    public string SupplierCode { get; set; }

    public bool? PortalManager { get; set; }

    public int? MaxUserCount { get; set; }

    public long? AppPortal { get; set; }

    public string NodeCode { get; set; }

    public string MasterSite { get; set; }

    public string SupplierName { get; set; }

    public string CustomerName { get; set; }

    public long? SupplierId { get; set; }

    public long? CustomerId { get; set; }

    public bool DomainUserOnly { get; set; }

    public bool IsCaenabled { get; set; }

    public string DomainName { get; set; }

    public string UuId { get; set; }

    public string Photo { get; set; }

    public string YhtUserId { get; set; }

    public string Salt { get; set; }

    public string YhtUserCode { get; set; }

    public bool? IsEnableMfa { get; set; }

    public string Mfakey { get; set; }

    public string Cust4SeeyonA8loginName { get; set; }
}
