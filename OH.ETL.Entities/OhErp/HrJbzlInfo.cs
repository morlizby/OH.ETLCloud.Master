using System;
using System.Collections.Generic;

namespace OH.ETL.Entities.OhErp;

public partial class HrJbzlInfo
{
    public int Id { get; set; }
    /// <summary>
    /// 属性类别
    /// </summary>
    public string Type { get; set; }
    /// <summary>
    /// 岗位名称
    /// </summary>
    public string Memo { get; set; }

    //public string Cdefine1 { get; set; }
    /// <summary>
    /// 业务类型
    /// </summary>
    public string Cdefine2 { get; set; }
}
