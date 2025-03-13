using System;
using System.Collections.Generic;

namespace OH.ETL.Entities.U9Erp;

public partial class CboJobHistoryTrl
{
    public long Id { get; set; }

    public string SysMlflag { get; set; }

    public string CompanyName { get; set; }

    public string Job { get; set; }

    public string JobGrade { get; set; }

    public string MajorAchievement { get; set; }

    public string Voucher { get; set; }

    public string VoucherContact { get; set; }

    public string DimissionReason { get; set; }

    public string DescFlexFieldCombineName { get; set; }
}
