using OH.ETL.Entities.SystemModels;

namespace OH.ETL.Entities.OhErp;

public partial class BranchMapper : OhErpEntity
{
    /// <summary>
    /// u9c组织ID
    /// </summary>
    public long? OrgId { get; set; }
    /// <summary>
    /// u9c组织代码
    /// </summary>
    public string OrgCode { get; set; }
    /// <summary>
    /// BranchId部门编码
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// 部门名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// u9c部门代码
    /// </summary>
    public long? U9dept { get; set; }
    /// <summary>
    /// u9c部门ID
    /// </summary>
    public string U9deptId { get; set; }

    //public string U9deptCode { get; set; }

    /// <summary>
    /// u9c部门名称
    /// </summary>
    public string U9deptName { get; set; }
    /// <summary>
    /// u9c部门类型
    /// </summary>
    public string U9deptType { get; set; }
    /// <summary>
    /// 创建日期
    /// </summary>
    public DateTime? CreateTime { get; set; }
}
