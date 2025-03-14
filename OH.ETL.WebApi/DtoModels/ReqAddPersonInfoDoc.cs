namespace OH.ETL.WebApi.DtoModels;

/// <summary>
/// 人员信息请求新增(U9C接口专用)
/// </summary>
public class ReqAddPersonInfoDoc
{
    /// <summary>
    /// 联系对象编码
    /// </summary>
    public string ContactCode { get; set; }

    /// <summary>
    /// 国籍编码
    /// </summary>
    public string NationalityCode { get; set; }

    /// <summary>
    /// 证件类别Enum
    /// </summary>
    public int CertificateType { get; set; }
    /// <summary>
    /// 证件号码(员工工号)
    /// </summary>
    public string PersonID { get; set; }
    /// <summary>
    /// 员工姓名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 性别标识
    /// </summary>
    public int? Sex { get; set; }
    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? Birthday { get; set; }
    /// <summary>
    /// 婚姻状况Enum
    /// </summary>
    public int? MarriageStatus { get; set; }
    /// <summary>
    /// 血型Enum
    /// </summary>
    public int? BloodType { get; set; }

    /// <summary>
    /// 学历编码
    /// </summary>
    public string DiplomaCode { get; set; }
    /// <summary>
    /// 学位编码
    /// </summary>
    public string DegreeCode { get; set; }

    /// <summary>
    /// 专业技术职务编码
    /// </summary>
    public string JobTtileCode { get; set; }

    /// <summary>
    /// 职业资格Enum
    /// </summary>
    public int? WorkQualification { get; set; }
    /// <summary>
    /// 就业日期(入职日期)
    /// </summary>
    public DateTime OccupationDate { get; set; }

    /// <summary>
    /// 宗教Enum
    /// </summary>
    public int? Religion { get; set; }
    /// <summary>
    /// 残障人士
    /// </summary>
    public bool? IsDeformity { get; set; }
    /// <summary>
    /// 离退休日期
    /// </summary>
    public DateTime? RetirTime { get; set; }
    /// <summary>
    /// 离退休年龄
    /// </summary>
    public decimal? RetireAge { get; set; }
    /// <summary>
    /// 工龄
    /// </summary>
    public decimal? WorkAge { get; set; }
    /// <summary>
    /// 工龄计算校正值
    /// </summary>
    public decimal? LOSReviseValue { get; set; }

    /// <summary>
    /// 出生地编码
    /// </summary>
    public string BornCountryCode { get; set; }

    /// <summary>
    /// 现居住地编码
    /// </summary>
    public string NowLivingCountryCode { get; set; }

    /// <summary>
    /// 保密
    /// </summary>
    public bool? IsSecrecy { get; set; }
    /// <summary>
    /// 中介名称DescFlexFieldPubDescSeg8
    /// </summary>
    public string PubDescSeg8 { get; set; }

    /// <summary>
    /// 个人身份Enum
    /// </summary>
    public int? PersonalStatus { get; set; }

    /// <summary>
    /// 民族编码*
    /// </summary>
    public string NationCode { get; set; }
    /// <summary>
    /// 政治面貌Enum
    /// </summary>
    public int? PoliticalStatus { get; set; }
    /// <summary>
    /// 加入党(团)日期
    /// </summary>
    public DateTime? JoinDate { get; set; }
    /// <summary>
    /// 现身份起始日期
    /// </summary>
    public DateTime? StatusStartDate { get; set; }
    /// <summary>
    /// 户口性质Enum
    /// </summary>
    public int? RegisteredResidenceType { get; set; }
    /// <summary>
    /// 户籍
    /// </summary>
    public string RegisteredResidence { get; set; }
    /// <summary>
    /// 籍贯
    /// </summary>
    public string NativePlace { get; set; }

    /// <summary>
    /// 地址编码
    /// </summary>
    public string AddressCode { get; set; }
    /// <summary>
    /// 固定电话
    /// </summary>
    public string TelPhone { get; set; }
    /// <summary>
    /// 移动电话
    /// </summary>
    public string MobilePhone { get; set; }
    /// <summary>
    /// 传真
    /// </summary>
    public string Fax { get; set; }
    /// <summary>
    /// 电子邮箱
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// 人才来源Enum
    /// </summary>
    public int? TalentSource { get; set; }
    /// <summary>
    /// 人才级别Enum
    /// </summary>
    public int? TalentLevel { get; set; }
    /// <summary>
    /// 入库日期
    /// </summary>
    public DateTime? InStoreDate { get; set; }

    /// <summary>
    /// 推荐人证件号码
    /// </summary>
    public string RefereesPersonID { get; set; }

    /// <summary>
    /// 资源编码
    /// </summary>
    public string ResourceCode { get; set; }

    /// <summary>
    /// 工作人事组织编码
    /// </summary>
    public string WorkingOrgCode { get; set; }

    /// <summary>
    /// 员工类别编码
    /// </summary>
    public string PersonCategoryCode { get; set; }
    /// <summary>
    /// 员工编号
    /// </summary>
    public string EmployeeCode { get; set; }

    /// <summary>
    /// 现任业务组织编码
    /// </summary>
    public string BusinessOrgCode { get; set; }
    /// <summary>
    /// 现任部门编码
    /// </summary>
    public string DeptCode { get; set; }
    /// <summary>
    /// 现任职务编码
    /// </summary>
    public string JobCode { get; set; }

    /// <summary>
    /// 现任岗位编码
    /// </summary>
    public string PositionCode { get; set; }
    /// <summary>
    /// 职级职等编码
    /// </summary>
    public string JobLevelCode { get; set; }

    /// <summary>
    /// 内部工龄
    /// </summary>
    public decimal? InnerAge { get; set; }
    /// <summary>
    /// 内部工龄校正值
    /// </summary>
    public decimal? InnerAgeValue { get; set; }

    /// <summary>
    /// 入职类型Enum
    /// </summary>
    public int? EntranceType { get; set; }
    /// <summary>
    /// 入职日期
    /// </summary>
    public DateTime EntranceDate { get; set; }
    /// <summary>
    /// 截止日期
    /// </summary>
    public DateTime? EntranceEndDate { get; set; }
    /// <summary>
    /// 入职渠道Enum
    /// </summary>
    public int? EntranceChannel { get; set; }

    /// <summary>
    /// 来源组织编码
    /// </summary>
    public string SourceOrgCode { get; set; }

    /// <summary>
    /// 来自招聘
    /// </summary>
    public bool? IsApplicant { get; set; }
    /// <summary>
    /// 任职类型Enum
    /// </summary>
    public int AssignType { get; set; }
    /// <summary>
    /// 试用
    /// </summary>
    public bool? IsProbation { get; set; }
    /// <summary>
    /// 离职日期
    /// </summary>
    public DateTime? DimissionDate { get; set; }
    /// <summary>
    /// 离职类型Enum
    /// </summary>
    public int? DimissionType { get; set; }
    /// <summary>
    /// 赔偿金额
    /// </summary>
    public decimal? Amende { get; set; }
    /// <summary>
    /// 补偿金额
    /// </summary>
    public decimal? Compensate { get; set; }

    /// <summary>
    /// 币种编码
    /// </summary>
    public string CurrencyCode { get; set; }

    /// <summary>
    /// 薪资结算日期
    /// </summary>
    public DateTime? SalaryAblanceTime { get; set; }
    /// <summary>
    /// 考勤截止日期
    /// </summary>
    public DateTime? EndCheckTime { get; set; }
    /// <summary>
    /// 上年收入小于等于6万
    /// </summary>
    public bool? IsUnder6wTax { get; set; }

    /// <summary>
    /// 主任职
    /// </summary>
    public bool? IsMain { get; set; }
    /// <summary>
    /// 任职开始日期
    /// </summary>
    public DateTime AssgnBeginDate { get; set; }
}
