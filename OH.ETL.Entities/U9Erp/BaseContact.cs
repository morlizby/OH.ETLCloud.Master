namespace OH.ETL.Entities.U9Erp;

public partial class BaseContact
{
    public long Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string ModifiedBy { get; set; }

    public int ContactType { get; set; }

    public string PersonNameFirstName { get; set; }

    public string PersonNameLastName { get; set; }

    public string PersonNameMiddleName { get; set; }

    public string PersonNameNickName { get; set; }

    public string PersonNameDisplayName { get; set; }

    public int? Gender { get; set; }

    public long? SysVersion { get; set; }

    public bool? EffectiveIsEffective { get; set; }

    public DateTime? EffectiveEffectiveDate { get; set; }

    public DateTime? EffectiveDisableDate { get; set; }

    public string Job { get; set; }

    public string Department { get; set; }

    public long? Enterprise { get; set; }

    public bool? IsEmailNotify { get; set; }

    public bool? IsMessageNotify { get; set; }

    public string Code { get; set; }

    public bool? IsOrgContact { get; set; }

    public bool? IsCustomerContact { get; set; }

    public bool? IsSupplierContact { get; set; }

    public bool? IsEmployeeContact { get; set; }

    public long? DefaultLocation { get; set; }

    public string DefaultPhoneNum { get; set; }

    public string DefaultMobilNum { get; set; }

    public string DefaultEmail { get; set; }

    public string DefaultFaxNum { get; set; }

    public string DefaultUrladdr { get; set; }

    public string MasterSite { get; set; }

    public string DefaultIms { get; set; }

    public byte[] Seal { get; set; }

    public bool? IsShowUserName { get; set; }
}
