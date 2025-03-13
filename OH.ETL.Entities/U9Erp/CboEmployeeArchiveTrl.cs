using System;
using System.Collections.Generic;

namespace OH.ETL.Entities.U9Erp;

public partial class CboEmployeeArchiveTrl
{
    public long Id { get; set; }

    public string SysMlflag { get; set; }

    public string DescFlexFieldCombineName { get; set; }

    public string Remark { get; set; }
}
