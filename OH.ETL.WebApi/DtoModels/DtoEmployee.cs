namespace OH.ETL.WebApi.DtoModels;

/// <summary>
/// OldErp人员档案信息
/// </summary>
public class DtoEmployee
{
    /// <summary>
    /// Id标识
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 组织代码
    /// </summary>
    public string OrgCode { get; set; }

    /// <summary>
    /// 人员Id
    /// </summary>
    public string UserId { get; set; }
    /// <summary>
    /// 姓名
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// 性别
    /// </summary>
    public string Sex { get; set; }
    /// <summary>
    /// 婚姻状况
    /// </summary>
    public string Marry { get; set; }
    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 员工状态:离职/在职
    /// </summary>
    public string Department { get; set; }
    /// <summary>
    /// 入职日期
    /// </summary>
    public DateTime? Duty { get; set; }
    /// <summary>
    /// 离职日期
    /// </summary>
    public DateTime? LzDate { get; set; }
    /// <summary>
    /// 离职类型
    /// </summary>
    public string Lztype { get; set; }
    /// <summary>
    /// 离职原因
    /// </summary>
    public string Lzmemo { get; set; }
    /// <summary>
    /// 岗位
    /// </summary>
    public string Job { get; set; }
    /// <summary>
    /// 职务
    /// </summary>
    public string Position { get; set; }
    /// <summary>
    /// 部门Id
    /// </summary>
    public string BranchId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string BranchName { get; set; }
    /// <summary>
    /// 证件号
    /// </summary>
    public string CardId { get; set; }
    /// <summary>
    /// 联系电话
    /// </summary>
    public string MovePhone { get; set; }
    /// <summary>
    /// 政治面貌
    /// </summary>
    public string Speciality { get; set; }
    /// <summary>
    /// 民族
    /// </summary>
    public string Mz { get; set; }

    /// <summary>
    /// 用工形式
    /// </summary>
    public string Ygtype { get; set; }
    /// <summary>
    /// 发薪方式:月薪,计件,计时
    /// </summary>
    public string Fxtype { get; set; }
    /// <summary>
    /// 最后更新日期
    /// </summary>
    public DateTime? Lastupdate { get; set; }
    /// <summary>
    /// 最后更新人
    /// </summary>
    public string Lastuser { get; set; }

}
