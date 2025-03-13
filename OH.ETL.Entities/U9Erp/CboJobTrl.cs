using System;
using System.Collections.Generic;

namespace OH.ETL.Entities.U9Erp;

public partial class CboJobTrl
{
    public long Id { get; set; }

    public string SysMlflag { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public string Description { get; set; }

    public string AliasName { get; set; }

    public string CombineName { get; set; }

    public string DescFlexFieldCombineName { get; set; }
}
