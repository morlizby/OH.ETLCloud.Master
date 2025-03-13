using System.ComponentModel.DataAnnotations;
using OH.ETL.Entities.SystemModels;

namespace OH.ETL.Entities.OhErp;

public partial class Branch : OhErpEntity
{
    /// <summary>
    /// 部门Id
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// 部门名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 父级Id
    /// </summary>
    public string ParentId { get; set; }
}
