using System;
using System.Collections.Generic;

namespace OH.ETL.Entities.U9Erp;

public partial class CboEmployeeAssignmentTrl
{
    public long Id { get; set; }

    public string SysMlflag { get; set; }

    public string PositionReasons { get; set; }

    public string Reasons { get; set; }

    public string ProSumUp { get; set; }

    public string DescFlexFieldCombineName { get; set; }
}
