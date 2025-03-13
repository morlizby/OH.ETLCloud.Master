namespace OH.ETL.Entities.U9Erp;

public partial class CboApplyForInfo
{
    public DateTime? CreatedOn { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string CreatedBy { get; set; }

    public string ModifiedBy { get; set; }

    public long Person { get; set; }

    public long? CreateOrg { get; set; }

    public long ApplyOrg { get; set; }

    public long? ApplyPosition { get; set; }

    public long Id { get; set; }

    public long? SysVersion { get; set; }

    public long? ApplyDoc { get; set; }

    public long? ApplyDept { get; set; }

    public DateTime ApplyDate { get; set; }

    public int? ApplyStatus { get; set; }

    public long? ApplyJob { get; set; }

    public long? ApplyBusinessOrg { get; set; }
}
