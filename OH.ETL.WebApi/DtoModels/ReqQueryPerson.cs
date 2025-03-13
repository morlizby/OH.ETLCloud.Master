namespace OH.ETL.WebApi.DtoModels;

/// <summary>
/// 人员信息请求查询体
/// </summary>
public class ReqQueryPerson
{
    /// <summary>
    /// 人员信息ID
    /// </summary>
    public long? iD { get; set; }
    /// <summary>
    /// 证件号码
    /// </summary>
    public string personID { get; set; }
    /// <summary>
    /// 人员姓名
    /// </summary>
    public string personName { get; set; }
    /// <summary>
    /// 工作人事组织
    /// </summary>
    public string workingOrg { get; set; }
    /// <summary>
    /// 员工编码
    /// </summary>
    public string employeeCode { get; set; }
    /// <summary>
    /// 现任业务组织
    /// </summary>
    public string businessOrg { get; set; }
    /// <summary>
    /// 现任部门编码
    /// </summary>
    public string deptCode { get; set; }
    /// <summary>
    /// 现任职务编码
    /// </summary>
    public string jobCode { get; set; }
    /// <summary>
    /// 现任岗位编码
    /// </summary>
    public string positionCode { get; set; }
    /// <summary>
    /// 入职类型Enum
    /// </summary>
    public string entranceType { get; set; }
}
