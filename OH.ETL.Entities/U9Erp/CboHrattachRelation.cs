using System;
using System.Collections.Generic;

namespace OH.ETL.Entities.U9Erp;

public partial class CboHrattachRelation
{
    public long Id { get; set; }

    public DateTime? CreatedOn { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? ModifiedOn { get; set; }

    public string ModifiedBy { get; set; }

    public long? SysVersion { get; set; }

    public long Person { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public long Org { get; set; }

    public bool IsSecrecy { get; set; }

    public long? FromOrg { get; set; }

    public long? ToOrg { get; set; }

    public int? EndActivity { get; set; }
}
